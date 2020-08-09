using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Application.Keyboards.Data;
using BetterTravel.Application.Keyboards.Factories;
using BetterTravel.Commands.Abstractions;
using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.MediatR.Core.HandlerResults.Abstractions;
using CSharpFunctionalExtensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Chat = BetterTravel.Domain.Entities.Chat;

namespace BetterTravel.Commands.Telegram.Settings
{
    public class SettingsCommandHandler : CommandHandlerBase<SettingsCommand>
    {
        private const string KeyboardMessage = "Configure your subscription settings here";
        
        private readonly ITelegramBotClient _telegram;
        
        public SettingsCommandHandler(
            IUnitOfWork unitOfWork, 
            ITelegramBotClient telegram) : base(unitOfWork) =>
            _telegram = telegram;

        public override async Task<IHandlerResult> Handle(
            SettingsCommand request, 
            CancellationToken ctx) =>
            await UnitOfWork.ChatRepository
                .GetFirstAsync(c => c.ChatId == request.ChatId)
                .ToResult("That chat wasn't found between our subscribers.")
                .Bind(GetKeyboardDataResult)
                .Bind(GetMarkupResult)
                .Tap(markup => SendMessageAsync(request.ChatId, KeyboardMessage, markup, ctx))
                .Finally(result => result.IsFailure
                    ? ValidationFailed(result.Error)
                    : Ok());

        private static Result<SettingsKeyboardData> GetKeyboardDataResult(Chat chat) => 
            Result.Ok(new SettingsKeyboardData {IsSubscribed = chat.Settings.IsSubscribed});
        
        private static Result<InlineKeyboardMarkup> GetMarkupResult(SettingsKeyboardData data) => 
            Result.Ok(new SettingsKeyboard().ConcreteKeyboardMarkup(data));

        private async Task<Message> SendMessageAsync(
            long chatId, string message, InlineKeyboardMarkup markup, CancellationToken token) => 
            await _telegram.SendTextMessageAsync(chatId, message, replyMarkup: markup, cancellationToken: token);
    }
}