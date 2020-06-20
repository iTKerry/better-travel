using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Commands.Abstractions;
using BetterTravel.MediatR.Core.HandlerResults.Abstractions;

namespace BetterTravel.Commands.Telegram.Subscribe
{
    public class SubscribeCommandHandler : CommandHandlerBase<SubscribeCommand>
    {
        public override Task<IHandlerResult> Handle(
            SubscribeCommand request, 
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}