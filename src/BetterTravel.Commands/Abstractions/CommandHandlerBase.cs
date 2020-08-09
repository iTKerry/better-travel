using BetterTravel.DataAccess.EF.Abstractions;
using BetterTravel.MediatR.Core;

namespace BetterTravel.Commands.Abstractions
{
    public abstract class CommandHandlerBase<TRequest> : RequestHandlerBase<TRequest> 
        where TRequest : ICommand
    {
        protected readonly IUnitOfWork UnitOfWork;
        
        protected CommandHandlerBase(IUnitOfWork unitOfWork) => 
            UnitOfWork = unitOfWork;
    }
}