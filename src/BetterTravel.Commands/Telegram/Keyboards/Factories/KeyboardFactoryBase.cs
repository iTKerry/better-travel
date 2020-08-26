using Telegram.Bot.Types.ReplyMarkups;

namespace BetterTravel.Commands.Telegram.Keyboards.Factories
{
    public abstract class KeyboardFactoryBase<T> where T : class
    {
        public abstract InlineKeyboardMarkup ConcreteKeyboardMarkup(T data);

        protected InlineKeyboardButton[][] Markup(params InlineKeyboardButton[][] lines) =>
            lines;
        
        protected InlineKeyboardButton[] Line(params InlineKeyboardButton[] buttons) =>
            buttons;
        
        protected InlineKeyboardButton Button(string text, string callbackData) =>
            new InlineKeyboardButton {Text = text, CallbackData = callbackData};
    }
}