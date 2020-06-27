using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BetterTravel.Queries.Departures;
using BetterTravel.Queries.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BetterTravel.Api.Controllers
{
    [Route("api/[controller]")]
    public class DeparturesController : ApiController
    {
        public DeparturesController(IMapper mapper, IMediator mediator) 
            : base(mapper, mediator)
        {
        }

        [HttpGet("all", Name = nameof(GetAllDepartures))]
        [ProducesResponseType(typeof(List<GetDeparturesViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllDepartures()
        {
            var query = new GetDeparturesQuery();
            var result = await Mediator.Send(query);
            return FromResult(result);
        }
    }
}