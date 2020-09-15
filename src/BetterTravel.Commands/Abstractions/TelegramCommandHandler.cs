using System.Threading;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Abstractions.Repository;
using BetterTravel.MediatR.Core.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BetterTravel.Commands.Abstractions
{
    public abstract class TelegramCommandHandler<TRequest> : CommandHandlerBase<TRequest>
        where TRequest : ICommand
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly ITelegramBotClient Client;

        protected TelegramCommandHandler(
            IUnitOfWork unitOfWork, ITelegramBotClient client) => 
            (UnitOfWork, Client) = 
            (unitOfWork, client);
        
        protected async Task<Message> SendMessageAsync(long chatId, string message, CancellationToken token) => 
            await Client.SendTextMessageAsync(chatId, message, cancellationToken: token);
        
        protected async Task<Message> SendMessageAsync(
            long chatId, string message, InlineKeyboardMarkup markup, CancellationToken token) => 
            await Client.SendTextMessageAsync(chatId, message, replyMarkup: markup, cancellationToken: token);

        protected async Task<Message> EditMessageTextAsync(
            long chatId, int messageId, string message, CancellationToken token) => 
            await Client.EditMessageTextAsync(chatId, messageId, message, cancellationToken: token);
        
        protected async Task<Message> EditMessageReplyMarkupAsync(
            long chatId, int messageId, InlineKeyboardMarkup markup, CancellationToken token) => 
            await Client.EditMessageReplyMarkupAsync(chatId, messageId, markup, token);

        protected async Task<Message> EditReplyMarkupAsync(
            long chatId, int messageId, InlineKeyboardMarkup markup, CancellationToken token) => 
            await Client.EditMessageReplyMarkupAsync(chatId, messageId, markup, token);
    }
}