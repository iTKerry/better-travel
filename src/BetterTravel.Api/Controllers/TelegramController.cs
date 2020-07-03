using System;
using System.Threading.Tasks;
using AutoMapper;
using BetterTravel.Commands.Abstractions;
using BetterTravel.Commands.Telegram.Settings;
using BetterTravel.Commands.Telegram.SettingsBack;
using BetterTravel.Commands.Telegram.SettingsCountries;
using BetterTravel.Commands.Telegram.SettingsCountriesSubscribe;
using BetterTravel.Commands.Telegram.SettingsCountriesUnsubscribe;
using BetterTravel.Commands.Telegram.SettingsDepartures;
using BetterTravel.Commands.Telegram.SettingsDeparturesSubscribe;
using BetterTravel.Commands.Telegram.SettingsDeparturesUnsubscribe;
using BetterTravel.Commands.Telegram.SettingsSubscribe;
using BetterTravel.Commands.Telegram.SettingsUnsubscribe;
using BetterTravel.Commands.Telegram.Start;
using BetterTravel.Commands.Telegram.Status;
using BetterTravel.MediatR.Core.HandlerResults;
using BetterTravel.MediatR.Core.HandlerResults.Abstractions;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BetterTravel.Api.Controllers
{
    [Route("api/[controller]")]
    public class TelegramController : ApiController
    {
        private readonly ITelegramBotClient _client;
        
        public TelegramController(IMapper mapper, IMediator mediator, ITelegramBotClient client) 
            : base(mapper, mediator) =>
            _client = client;

        [HttpPost("update/{token}")]
        public async Task Update([FromRoute] string token, [FromBody] Update update)
        {
            var maybeCommand = GetCommand(update);
            if (maybeCommand.HasNoValue)
                return;
            
            var result = await Mediator.Send(maybeCommand.Value);

            Maybe<long?> maybeChatId = update.Message?.Chat?.Id ?? update.CallbackQuery?.Message.Chat?.Id;
            if (maybeChatId.HasValue)
                await HandleResultAsync(result, maybeChatId.Value ?? 0);
        }

        private Maybe<ICommand> GetCommand(Update update) =>
            update.Type switch
            {
                UpdateType.Message => update.Message.Text.Replace("@BetterTravelBot", string.Empty) switch
                {
                    "/start" => Mapper.Map<StartCommand>(update),
                    "/subscribe" => Mapper.Map<SettingsSubscribeCommand>(update),
                    "/unsubscribe" => Mapper.Map<SettingsUnsubscribeCommand>(update),
                    "/status" => Mapper.Map<StatusCommand>(update),
                    "/settings" => Mapper.Map<SettingsCommand>(update),
                    _ => null
                },
                UpdateType.CallbackQuery => update.CallbackQuery.Data switch
                {
                    "SettingsSubscribe" => Mapper.Map<SettingsSubscribeCommand>(update),
                    "SettingsUnsubscribe" => Mapper.Map<SettingsUnsubscribeCommand>(update),
                    "SettingsCountries" => Mapper.Map<SettingsCountriesCommand>(update),
                    "SettingsDepartures" => Mapper.Map<SettingsDeparturesCommand>(update),
                    "SettingsBack" => Mapper.Map<SettingsBackCommand>(update),
                    var str when str?.Contains("SettingsCountrySubscribe") ?? false => 
                        Mapper.Map<SettingsCountriesSubscribeCommand>(update),
                    var str when str?.Contains("SettingsCountryUnsubscribe") ?? false => 
                        Mapper.Map<SettingsCountriesUnsubscribeCommand>(update),
                    var str when str?.Contains("SettingsDeparturesSubscribe") ?? false => 
                        Mapper.Map<SettingsDeparturesSubscribeCommand>(update),
                    var str when str?.Contains("SettingsDeparturesUnsubscribe") ?? false => 
                        Mapper.Map<SettingsDeparturesUnsubscribeCommand>(update),
                    _ => throw new InvalidOperationException()
                },
                _ => null
            };

        private async Task<Message> HandleResultAsync(IHandlerResult result, long chatId) =>
            result switch
            {
                ValidationFailedHandlerResult vf => await _client.SendTextMessageAsync(chatId, vf.Message),
                _ => null
            };
    }
}