using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Commands.Abstractions;
using BetterTravel.MediatR.Core.HandlerResults.Abstractions;

namespace BetterTravel.Commands.Telegram.Status
{
    public class StatusCommandHandler : CommandHandlerBase<StatusCommand>
    {
        public override Task<IHandlerResult> Handle(
            StatusCommand request, 
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}