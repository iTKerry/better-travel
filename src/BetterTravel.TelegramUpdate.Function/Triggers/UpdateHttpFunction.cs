using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using BetterTravel.MediatR.Core.Abstractions;
using BetterTravel.MediatR.Core.HandlerResults;
using BetterTravel.TelegramUpdate.Function.Commands.Settings;
using BetterTravel.TelegramUpdate.Function.Commands.SettingsBack;
using BetterTravel.TelegramUpdate.Function.Commands.SettingsCountries;
using BetterTravel.TelegramUpdate.Function.Commands.SettingsCountryToggle;
using BetterTravel.TelegramUpdate.Function.Commands.SettingsCurrency;
using BetterTravel.TelegramUpdate.Function.Commands.SettingsCurrencySwitch;
using BetterTravel.TelegramUpdate.Function.Commands.SettingsDepartures;
using BetterTravel.TelegramUpdate.Function.Commands.SettingsDepartureToggle;
using BetterTravel.TelegramUpdate.Function.Commands.SettingsSubscriptionToggle;
using BetterTravel.TelegramUpdate.Function.Commands.Start;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BetterTravel.TelegramUpdate.Function.Triggers
{
    public class UpdateHttpFunction
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ITelegramBotClient _client;

        public UpdateHttpFunction(IMediator mediator, IMapper mapper, ITelegramBotClient client)
        {
            _mediator = mediator;
            _mapper = mapper;
            _client = client;
        }

        [FunctionName(nameof(UpdateHttpFunction))]
        public async Task RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "update")]
            HttpRequest req, ILogger log)
        {
            try
            {
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var update = JsonConvert.DeserializeObject<Update>(requestBody);
            
                await GetCommand(update)
                    .MapWithTransactionScope(cmd => _mediator.Send(cmd))
                    .Bind(result =>
                        GetChatId(update)
                            .ToResult("There is no ChatID")
                            .Bind(chatId => Result.Success((Result: result, ChatId: chatId))))
                    .Tap(tuple => HandleResultAsync(tuple.Result, tuple.ChatId))
                    .OnFailure(message => log.LogError(message));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
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
                {"/start", message => _mapper.Map<StartCommand>(message)},
                {"/settings", message => _mapper.Map<SettingsCommand>(message)},
            };
        
        private Dictionary<string, Func<CallbackQuery, ICommand>> CallbackQueryCommands =>
            new Dictionary<string, Func<CallbackQuery, ICommand>>
            {
                {
                    nameof(SettingsSubscriptionToggleCommand), 
                    query => _mapper.Map<SettingsSubscriptionToggleCommand>(query)
                },
                {
                    nameof(SettingsCountriesCommand), 
                    query => _mapper.Map<SettingsCountriesCommand>(query)
                },
                {
                    nameof(SettingsCountryToggleCommand), 
                    query => _mapper.Map<SettingsCountryToggleCommand>(query)
                },
                {
                    nameof(SettingsDeparturesCommand), 
                    query => _mapper.Map<SettingsDeparturesCommand>(query)
                },
                {
                    nameof(SettingsDepartureToggleCommand), 
                    query => _mapper.Map<SettingsDepartureToggleCommand>(query)
                },
                {
                    nameof(SettingsCurrencyCommand),
                    query => _mapper.Map<SettingsCurrencyCommand>(query)
                },
                {
                    nameof(SettingsCurrencySwitchCommand),
                    query => _mapper.Map<SettingsCurrencySwitchCommand>(query)
                },
                {
                    nameof(SettingsBackCommand), 
                    query => _mapper.Map<SettingsBackCommand>(query)
                },
            };
    }
}