using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Commands.Abstractions;
using BetterTravel.MediatR.Core.HandlerResults.Abstractions;

namespace BetterTravel.Commands.Telegram.Unsubscribe
{
    public class UnsubscribeCommandHandler : CommandHandlerBase<UnsubscribeCommand>
    {
        public override Task<IHandlerResult> Handle(
            UnsubscribeCommand request, 
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}