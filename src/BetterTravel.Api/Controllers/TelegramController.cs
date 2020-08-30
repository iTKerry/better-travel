using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BetterTravel.Commands.Telegram.Settings;
using BetterTravel.Commands.Telegram.SettingsBack;
using BetterTravel.Commands.Telegram.SettingsCountries;
using BetterTravel.Commands.Telegram.SettingsCountryToggle;
using BetterTravel.Commands.Telegram.SettingsCurrency;
using BetterTravel.Commands.Telegram.SettingsCurrencySwitch;
using BetterTravel.Commands.Telegram.SettingsDepartures;
using BetterTravel.Commands.Telegram.SettingsDepartureToggle;
using BetterTravel.Commands.Telegram.SettingsSubscriptionToggle;
using BetterTravel.Commands.Telegram.Start;
using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.MediatR.Core.Abstractions;
using BetterTravel.MediatR.Core.HandlerResults;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BetterTravel.Api.Controllers
{
    [Route("api/update")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class TelegramController : ApiController
    {
        private readonly ITelegramBotClient _client;
        private readonly ILogger<TelegramController> _log;
        
        public TelegramController(
            IMediator mediator, IMapper mapper, 
            ITelegramBotClient client, IUnitOfWork unitOfWork,
            ILogger<TelegramController> log) 
            : base(mapper, mediator, unitOfWork) => 
            (_client, _log) = (client, log);

        [HttpPost("{token}")]
        public async Task Update([FromRoute] string token, [FromBody] Update update) =>
            await GetCommand(update)
                .MapWithTransactionScope(cmd => Mediator.Send(cmd))
                .Bind(result =>
                    GetChatId(update)
                        .ToResult("There is no ChatID")
                        .Bind(chatId => Result.Success((Result: result, ChatId: chatId))))
                .Tap(tuple => HandleResultAsync(tuple.Result, tuple.ChatId))
                .OnFailure(message => _log.LogError(message));

        private Result<ICommand> GetCommand(Update update) =>
            update.Type switch
            {
                UpdateType.Message => FromMessage(update.Message),
                UpdateType.CallbackQuery => FromCallbackQuery(update.CallbackQuery),
                _ => Result.Failure<ICommand>("Not supported chat type")
            };

        private Result<ICommand> FromMessage(Message message) =>
            MessageCommands
                .TryFirst(command => IsMatchedCommand(message.Text, command.Key))
                .ToResult("There is no such Message command")
                .Bind(command => Result.Success(command.Value(message)));

        private Result<ICommand> FromCallbackQuery(CallbackQuery callbackQuery) =>
            CallbackQueryCommands
                .TryFirst(command => IsMatchedCommand(callbackQuery.Data, command.Key))
                .ToResult("There is no such CallbackQuery command")
                .Bind(command => Result.Success(command.Value(callbackQuery)));

        private static bool IsMatchedCommand(string str, string command) =>
            str.Replace("@BetterTravelBot", string.Empty).Equals(command) || 
            str.Contains(':') && str.StartsWith(command);
        
        private static Maybe<long> GetChatId(Update update) =>
            update.Message?.Chat?.Id 
            ?? update.CallbackQuery?.Message.Chat?.Id 
            ?? Maybe<long>.None;
        
        private async Task<Maybe<Message>> HandleResultAsync(IHandlerResult result, long chatId) =>
            result switch
            {
                ValidationFailedHandlerResult vf => await _client.SendTextMessageAsync(chatId, vf.Message),
                _ => Maybe<Message>.None
            };

        private Dictionary<string, Func<Message, ICommand>> MessageCommands =>
            new Dictionary<string, Func<Message, ICommand>>
            {
                {"/start", message => Mapper.Map<StartCommand>(message)},
                {"/settings", message => Mapper.Map<SettingsCommand>(message)},
            };
        
        private Dictionary<string, Func<CallbackQuery, ICommand>> CallbackQueryCommands =>
            new Dictionary<string, Func<CallbackQuery, ICommand>>
            {
                {
                    nameof(SettingsSubscriptionToggleCommand), 
                    query => Mapper.Map<SettingsSubscriptionToggleCommand>(query)
                },
                {
                    nameof(SettingsCountriesCommand), 
                    query => Mapper.Map<SettingsCountriesCommand>(query)
                },
                {
                    nameof(SettingsCountryToggleCommand), 
                    query => Mapper.Map<SettingsCountryToggleCommand>(query)
                },
                {
                    nameof(SettingsDeparturesCommand), 
                    query => Mapper.Map<SettingsDeparturesCommand>(query)
                },
                {
                    nameof(SettingsDepartureToggleCommand), 
                    query => Mapper.Map<SettingsDepartureToggleCommand>(query)
                },
                {
                    nameof(SettingsCurrencyCommand),
                    query => Mapper.Map<SettingsCurrencyCommand>(query)
                },
                {
                    nameof(SettingsCurrencySwitchCommand),
                    query => Mapper.Map<SettingsCurrencySwitchCommand>(query)
                },
                {
                    nameof(SettingsBackCommand), 
                    query => Mapper.Map<SettingsBackCommand>(query)
                },
            };
    }
}