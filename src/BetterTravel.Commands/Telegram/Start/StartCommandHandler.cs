using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Commands.Abstractions;
using BetterTravel.DataAccess.Abstraction.Repositories;
using BetterTravel.MediatR.Core.HandlerResults.Abstractions;

namespace BetterTravel.Commands.Telegram.Start
{
    public class StartCommandHandler : CommandHandlerBase<StartCommand>
    {
        public StartCommandHandler(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
        
        public override async Task<IHandlerResult> Handle(
            StartCommand request, 
            CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}