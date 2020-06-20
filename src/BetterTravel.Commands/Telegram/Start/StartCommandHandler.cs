using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Commands.Abstractions;
using BetterTravel.MediatR.Core.HandlerResults.Abstractions;

namespace BetterTravel.Commands.Telegram.Start
{
    public class StartCommandHandler : CommandHandlerBase<StartCommand>
    {
        public override Task<IHandlerResult> Handle(
            StartCommand request, 
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}