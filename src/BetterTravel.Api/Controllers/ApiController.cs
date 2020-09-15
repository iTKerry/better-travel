using System;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using BetterTravel.Common.Utils;
using BetterTravel.DataAccess.Abstractions.Repository;
using BetterTravel.MediatR.Core.Abstractions;
using BetterTravel.MediatR.Core.HandlerResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BetterTravel.Api.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    public abstract class ApiController : ControllerBase
    {
        protected IMapper Mapper { get; }
        protected IMediator Mediator { get; }
        protected IUnitOfWork UnitOfWork { get; }

        protected ApiController(IMapper mapper, IMediator mediator, IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            (Mapper, Mediator) = (mapper, mediator);
        }

        protected async Task<IActionResult> FromResult<T>(IHandlerResult<T> result) =>
            result switch
            {
                DataHandlerResult<T> data => await Ok(data.Data),
                NotFoundHandlerResult<T> _ => NotFound(),
                PagedDataHandlerResult<T> pagedData => await Ok(pagedData),
                ValidationFailedHandlerResult<T> failed => Error(failed.Message),

                _ => throw new InvalidOperationException(),
            };
        
        protected async Task<IActionResult> FromResult(IHandlerResult result) =>
            result switch
            {
                OkHandlerResult _ => await Ok(),
                NotFoundHandlerResult _ => NotFound(),
                ValidationFailedHandlerResult vfResult => Error(vfResult.Message),
                
                _ => throw new InvalidOperationException(),
            };

        protected new async Task<IActionResult> Ok()
        {
            await UnitOfWork.CommitAsync();
            return base.Ok(Envelope.Ok());
        }
        
        protected async Task<IActionResult> Ok<T>(T value)
        {
            await UnitOfWork.CommitAsync();
            return base.Ok(Envelope.Ok(value));
        }

        protected new IActionResult NotFound()
        {
            return base.NotFound(Envelope.Ok());
        }

        protected IActionResult Error(string errorMessage)
        {
            return BadRequest(Envelope.Error(errorMessage));
        }
    }
}