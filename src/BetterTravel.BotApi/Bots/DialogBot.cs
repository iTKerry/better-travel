using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;

namespace BetterTravel.BotApi.Bots
{
    public sealed class DialogBot<T> : ActivityHandler where T : Dialog
    {
        private readonly ILogger _logger;

        public DialogBot(ILogger<DialogBot<T>> logger)
        {
            _logger = logger;
        }

        protected override Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            return base.OnMembersAddedAsync(membersAdded, turnContext, cancellationToken);
        }

        public override Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = new CancellationToken())
        {
            return base.OnTurnAsync(turnContext, cancellationToken);
        }

        protected override Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            return base.OnMessageActivityAsync(turnContext, cancellationToken);
        }
    }
}