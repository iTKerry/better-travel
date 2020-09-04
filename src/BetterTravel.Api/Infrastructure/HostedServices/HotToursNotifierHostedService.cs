using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BetterExtensions.AspNet.HostedServices;
using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.DataAccess.EF.Common;
using BetterTravel.DataAccess.Entities;
using BetterTravel.DataAccess.Redis.Abstractions;
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

            var chats = await unitOfWork.ChatRepository.GetAllAsync();

            await cache.GetValuesAsync()
                .Map(x => x.Select(data => data.EntityId).ToList())
                .Map(GetHotTours)
                .Map(tours => chats
                    .Select(chat => (chat.ChatId, Tours: tours
                        .Where(x =>
                            chat.IsSubscribedToCountry(x.Country) &&
                            chat.IsSubscribedToDeparture(x.DepartureLocation))
                        .ToList()))
                    .Where(x => x.Tours.Any())
                    .ToList())
                .Tap(x => x.Select(t => SendPictureAsync(t.ChatId, t.Tours, telegram)).WhenAll());
            
            async Task<List<HotTour>> GetHotTours(List<int> entityIds) =>
                await unitOfWork.HotToursRepository.GetAsync(new QueryObject<HotTour>
                {
                    Top = 10,
                    WherePredicate = tour => entityIds.Contains(tour.Id)
                });

            static async Task SendPictureAsync(long chatId, List<HotTour> tours, ITelegramBotClient client) =>
                await tours
                    .Select(x => client.SendPhotoAsync(chatId, new InputOnlineFile(x.Info.ImageUri), x.Info.Name))
                    .WhenAll();
        }
    }

    public static class Helpers
    {
        public static async Task WhenAll(this IEnumerable<Task> tasks) => 
            await Task.WhenAll(tasks);
    }
}