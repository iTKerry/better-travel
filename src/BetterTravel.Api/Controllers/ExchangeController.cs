using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BetterTravel.Common.Utils;
using BetterTravel.DataAccess.Abstractions.Repository;
using BetterTravel.Queries.Exchange.GetExchange;
using BetterTravel.Queries.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BetterTravel.Api.Controllers
{
    [Route("api/[controller]")]
    public class ExchangeController : ApiController
    {
        public ExchangeController(IMapper mapper, IMediator mediator, IUnitOfWork unitOfWork) 
            : base(mapper, mediator, unitOfWork)
        {
        }

        /// <summary>
        /// Get exchange rates for all supported currencies
        /// </summary>
        /// <returns>Exchange rates</returns>
        [HttpGet("get", Name = nameof(GetExchange))]
        [ProducesResponseType(typeof(Envelope<List<GetExchangeViewModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetExchange()
        {
            var query = new GetExchangeQuery();
            var result = await Mediator.Send(query);
            return await FromResult(result);
        }
    }
}