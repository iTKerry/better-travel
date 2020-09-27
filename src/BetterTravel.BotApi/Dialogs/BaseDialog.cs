using Microsoft.Bot.Builder.Dialogs;

namespace BetterTravel.BotApi.Dialogs
{
    public abstract class BaseDialog : ComponentDialog
    {
        protected abstract void InitializeDialog();
    }
}
