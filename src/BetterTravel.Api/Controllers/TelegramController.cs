using System;
using System.Collections.Generic;
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
using BetterTravel.MediatR.Core.HandlerResults;
using BetterTravel.MediatR.Core.HandlerResults.Abstractions;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BetterTravel.Api.Controllers
{
    [Route("api/[controller]")]
    public class TelegramController : ApiController
    {
        private readonly ITelegramBotClient _client;
        private readonly ILogger _logger;

        public TelegramController(IMapper mapper, IMediator mediator, ITelegramBotClient client)
            : base(mapper, mediator) =>
            (_client, _logger) = (client, Log.ForContext<TelegramController>());

        [HttpPost("update/{token}")]
        public async Task Update([FromRoute] string token, [FromBody] Update update) =>
            await GetCommand(update)
                .MapWithTransactionScope(cmd => Mediator.Send(cmd))
                .Bind(result =>
                    GetChatId(update)
                        .ToResult("There is no ChatID")
                        .Bind(chatId => Result.Ok((Result: result, ChatId: chatId))))
                .Tap(tuple => HandleResultAsync(tuple.Result, tuple.ChatId))
                .OnFailure(message => _logger.Error(message));

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
                .Bind(command => Result.Ok(command.Value(message)));

        private Result<ICommand> FromCallbackQuery(CallbackQuery callbackQuery) =>
            CallbackQueryCommands
                .TryFirst(command => IsMatchedCommand(callbackQuery.Data, command.Key))
                .ToResult("There is no such CallbackQuery command")
                .Bind(command => Result.Ok(command.Value(callbackQuery)));

        private static bool IsMatchedCommand(string str, string command) =>
            str.Equals(command) || 
            str.Contains(':') && str.StartsWith(command);
        
        private static Maybe<long> GetChatId(Update update) =>
            update.Message?.Chat?.Id 
            ?? update.CallbackQuery?.Message.Chat?.Id 
            ?? Maybe<long>.None;

        private async Task<Message> HandleResultAsync(IHandlerResult result, long chatId) =>
            result switch
            {
                ValidationFailedHandlerResult vf => await _client.SendTextMessageAsync(chatId, vf.Message),
                _ => null
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
                {"SettingsSubscribe", query => Mapper.Map<SettingsSubscribeCommand>(query)},
                {"SettingsUnsubscribe", query => Mapper.Map<SettingsUnsubscribeCommand>(query)},
                {"SettingsCountries", query => Mapper.Map<SettingsCountriesCommand>(query)},
                {"SettingsDepartures", query => Mapper.Map<SettingsDeparturesCommand>(query)},
                {"SettingsBack", query => Mapper.Map<SettingsBackCommand>(query)},
                {"SettingsCountrySubscribe", query => Mapper.Map<SettingsCountriesSubscribeCommand>(query)},
                {"SettingsCountryUnsubscribe", query => Mapper.Map<SettingsCountriesUnsubscribeCommand>(query)},
                {"SettingsDeparturesSubscribe", query => Mapper.Map<SettingsDeparturesSubscribeCommand>(query)},
                {"SettingsDeparturesUnsubscribe", query => Mapper.Map<SettingsDeparturesUnsubscribeCommand>(query)},
            };
    }
}