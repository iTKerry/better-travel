using System;
using System.Net.Mime;
using AutoMapper;
using BetterTravel.MediatR.Core.HandlerResults;
using BetterTravel.MediatR.Core.HandlerResults.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BetterTravel.Api.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    public abstract class ApiController : ControllerBase
    {
        protected IMapper Mapper { get; }
        protected IMediator Mediator { get; }

        protected ApiController(IMapper mapper, IMediator mediator) =>
            (Mapper, Mediator) = (mapper, mediator);

        protected IActionResult FromResult<T>(IHandlerResult<T> result) where T : class =>
            result switch
            {
                OkHandlerResult<T> ok => Ok(ok.Data) as IActionResult,
                NotFoundHandlerResult<T> _ => NotFound() as IActionResult,
                ValidationFailedHandlerResult<T> vf => BadRequest(vf.Message) as IActionResult, 
                
                _ => throw new InvalidOperationException(),
            };
        
        protected IActionResult FromResult(IHandlerResult result) =>
            result switch
            {
                OkHandlerResult _ => Ok() as IActionResult,
                NotFoundHandlerResult _ => NotFound() as IActionResult,
                ValidationFailedHandlerResult vf => BadRequest(vf.Message) as IActionResult,
                
                _ => throw new InvalidOperationException(),
            };
    }
}