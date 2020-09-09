using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BetterExtensions.AspNet.HostedServices;
using BetterTravel.Application.Exchange.Abstractions;
using BetterTravel.Common.Localization;
using BetterTravel.Common.Utils;
using BetterTravel.DataAccess.Cache;
using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.DataAccess.EF.Common;
using BetterTravel.DataAccess.Entities;
using BetterTravel.DataAccess.Entities.Enumerations;
using BetterTravel.DataAccess.Redis.Abstractions;
using BetterTravel.DataAccess.ValueObjects;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;

namespace BetterTravel.Api.Infrastructure.HostedServices
{
    public class HotToursNotifierHostedService : TimedHostedService<HotToursNotifierHostedService>
    {
        protected override TimeSpan DueTime => TimeSpan.FromMinutes(1);
        protected override TimeSpan Period => TimeSpan.FromMinutes(5);
        
        public HotToursNotifierHostedService(
            IServiceProvider services, 
            ILogger<HotToursNotifierHostedService> logger) 
            : base(services, logger)
        {
        }

        protected override async Task JobAsync(IServiceScope serviceScope)
        {
            var telegram = serviceScope.ServiceProvider.GetRequiredService<ITelegramBotClient>();
            var unitOfWork = serviceScope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var cache = serviceScope.ServiceProvider.GetRequiredService<IHotTourFoundRepository>();
            var exchangeProvider = serviceScope.ServiceProvider.GetRequiredService<IExchangeProvider>();
            
            var exchangeRateResult = await exchangeProvider.GetExchangeAsync();
            if (exchangeRateResult.IsFailure)
            {
                _logger.LogError(exchangeRateResult.Error);
                return;
            }
            
            var exchangeRates = exchangeRateResult.Value;
            var chats = await unitOfWork.ChatRepository.GetAllAsync();

            await cache.GetValuesAsync()
                .Map(cachedFoundTours => cachedFoundTours.Select(data => data.EntityId).ToList())
                .Map(entityIds => new QueryObject<HotTour> {WherePredicate = tour => entityIds.Contains(tour.Id)})
                .Map(queryObject => unitOfWork.HotToursRepository.GetAsync(queryObject))
                .Map(tours => chats
                    .Select(chat => (
                        Chat: chat,
                        Tours: ExtractChatTours(chat, tours),
                        Exchange: GetExchangeForChat(chat, exchangeRates)))
                    .Where(t => t.Tours.Any())
                    .ToList())
                .Bind(list => Result
                    .FailureIf(
                        list.Any(t => t.Exchange.HasNoValue),
                        "At lease one chat doesn't have exchange rate")
                    .Map(() => list.Select(t => (t.Chat, t.Tours, Exchange: t.Exchange.Value))))
                .Tap(list => list
                    .SelectMany(t => GetPictureMessage(t.Chat, t.Tours, t.Exchange))
                    .Select(m => SendPictureAsync(m.ChatId, m.PictureUri, m.Caption, telegram))
                    .WhenAll())
                .Tap(async () => await cache.CleanAsync());
        }

        private static List<HotTour> ExtractChatTours(Chat chat, List<HotTour> tours) =>
            tours
                .Where(x =>
                    chat.CanReceiveUpdatesFromCountry(x.Country) &&
                    chat.CanReceiveUpdatesFromDeparture(x.DepartureLocation))
                .Take(10)
                .ToList();

        private static Maybe<CurrencyRate> GetExchangeForChat(Chat chat, List<CurrencyRate> currencyRates) => 
            currencyRates.FirstOrDefault(currencyRate => chat.Settings.Currency.IsType(currencyRate.Type));

        private static List<PictureMessage> GetPictureMessage(
            Chat chat, List<HotTour> tours, CurrencyRate currencyRate) =>
            tours.Select(tour =>
            {
                var priceAmount = GetPriceAmount(tour.Price, chat.Settings.Currency, currencyRate);
                var caption = FormatTourInfo(tour, chat.Settings.Currency, priceAmount);
                return new PictureMessage
                {
                    ChatId = chat.ChatId,
                    PictureUri = tour.Info.ImageUri,
                    Caption = caption
                };
            }).ToList();

        private static double GetPriceAmount(Price price, Currency chatCurrency, CurrencyRate currencyRate) =>
            price.Currency switch
            {
                var tc when tc == chatCurrency => price.Amount,
                var tc when tc == Currency.UAH => Math.Round(price.Amount / currencyRate.Rate, 2),
                _ => throw new InvalidOperationException()
            };
        
        private static string FormatTourInfo(HotTour hotTour, Currency currency, double priceAmount) =>
            new[]
            {
                $"{hotTour.Info.Name} {(int) hotTour.HotelCategory}*",
                $"🌴 {L.GetValue(hotTour.Country.Name, Culture.Ru)} - {hotTour.Resort.Name}",
                $"🌙 {hotTour.Duration.Count} {L.GetValue(hotTour.Duration.Type.ToString(), Culture.Ru)}",
                $"💰 {currency.Sign}{priceAmount:N2} / {L.GetValue(hotTour.Price.Type.ToString(), Culture.Ru)}",
                $"✈ {L.GetValue(Localization.from, Culture.Ru)} {L.GetValue(hotTour.DepartureLocation.Name, Culture.Ru)}",
                $"{hotTour.Info.DetailsUri}"
            }.Aggregate((left, right) => $"{left}\n{right}");

        private static async Task SendPictureAsync(
            long chatId, Uri pictureUri, string text, ITelegramBotClient client) =>
            await client.SendPhotoAsync(chatId, new InputOnlineFile(pictureUri), text);
        
        private class PictureMessage
        {
            public long ChatId { get; set; }
            public Uri PictureUri { get; set; }
            public string Caption { get; set; }
        }
    }
}