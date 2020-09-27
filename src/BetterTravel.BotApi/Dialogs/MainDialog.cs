using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;

namespace BetterTravel.BotApi.Dialogs
{
    public sealed class MainDialog : BaseDialog
    {
        protected override void InitializeDialog()
        {
            var waterfallSteps = new WaterfallStep[]
            {
                InitialStepAsync,
                FinalStepAsync
            };

            AddDialog(new WaterfallDialog($"{nameof(MainDialog)}.mainFlow", waterfallSteps));

            InitialDialogId = $"{nameof(MainDialog)}.mainFlow";
        }

        private Task<DialogTurnResult> InitialStepAsync(
            WaterfallStepContext stepContext,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        private static async Task<DialogTurnResult> FinalStepAsync(
            WaterfallStepContext stepContext,
            CancellationToken cancellationToken)
        {
            return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
        }
    }
}