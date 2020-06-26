using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BetterTravel.Api.Requests.HotTours;
using BetterTravel.Queries.HotTours;
using BetterTravel.Queries.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BetterTravel.Api.Controllers
{
    [Route("api/[controller]")]
    public class HotToursController : ApiController
    {
        public HotToursController(IMapper mapper, IMediator mediator) 
            : base(mapper, mediator)
        {
        }

        [HttpGet("get", Name = nameof(GetTours))]
        [ProducesResponseType(typeof(List<GetHotToursViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTours([FromQuery] GetHotToursDto data)
        {
            var query = Mapper.Map<GetHotToursQuery>(data);
            var result = await Mediator.Send(query);
            return FromResult(result);
        }
    }
}