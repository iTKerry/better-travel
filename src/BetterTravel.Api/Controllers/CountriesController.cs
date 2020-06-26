using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BetterTravel.Queries.Countries;
using BetterTravel.Queries.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BetterTravel.Api.Controllers
{
    [Route("api/[controller]")]
    public class CountriesController : ApiController
    {
        public CountriesController(IMapper mapper, IMediator mediator) : base(mapper, mediator)
        {
        }

        [HttpGet("all", Name = nameof(GetAllCountries))]
        [ProducesResponseType(typeof(List<GetCountriesViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCountries()
        {
            var query = new GetCountriesQuery();
            var result = await Mediator.Send(query);
            return FromResult(result);
        }
    }
}