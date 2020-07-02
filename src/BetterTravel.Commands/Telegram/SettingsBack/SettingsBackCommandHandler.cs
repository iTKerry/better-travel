using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Commands.Abstractions;
using BetterTravel.Commands.Telegram.Settings;
using BetterTravel.DataAccess.Repositories;
using BetterTravel.MediatR.Core.HandlerResults.Abstractions;
using CSharpFunctionalExtensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Chat = BetterTravel.DataAccess.Entities.Chat;

namespace BetterTravel.Commands.Telegram.SettingsBack
{
    public class SettingsBackCommandHandler : CommandHandlerBase<SettingsBackCommand>
    {
        private readonly ITelegramBotClient _telegram;
        
        public SettingsBackCommandHandler(
            IUnitOfWork unitOfWork, 
            ITelegramBotClient telegram) : base(unitOfWork) =>
            _telegram = telegram;

        public override async Task<IHandlerResult> Handle(
            SettingsBackCommand request, 
            CancellationToken cancellationToken) =>
            await UnitOfWork.ChatRepository
                .GetFirstAsync(c => c.ChatId == request.ChatId)
                .ToResult("That chat wasn't found between our subscribers.")
                .Bind(GetKeyboardDataResult)
                .Bind(GetMarkupResult)
                .Tap(markup => EditMessageReplyMarkupAsync(request.ChatId, request.MessageId, markup, cancellationToken))
                .Finally(result => result.IsFailure 
                    ? ValidationFailed(result.Error) 
                    : Ok());
        
        private static Result<SettingsKeyboardData> GetKeyboardDataResult(Chat chat) => 
            Result.Ok(new SettingsKeyboardData {IsSubscribed = chat.Settings.IsSubscribed});
        
        private static Result<InlineKeyboardMarkup> GetMarkupResult(SettingsKeyboardData data) => 
            Result.Ok(new SettingsKeyboard().ConcreteKeyboardMarkup(data));

        private async Task<Message> EditMessageReplyMarkupAsync(
            long chatId, int messageId, InlineKeyboardMarkup markup, CancellationToken token) => 
            await _telegram.EditMessageReplyMarkupAsync(chatId, messageId, markup, token);
    }
}