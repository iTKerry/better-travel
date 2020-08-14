using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BetterTravel.Api.Queries.HotTours.GetCountries;
using BetterTravel.Api.Queries.HotTours.GetDepartures;
using BetterTravel.Api.Queries.HotTours.GetHotelCategories;
using BetterTravel.Api.Queries.HotTours.GetHotTours;
using BetterTravel.Api.Queries.ViewModels;
using BetterTravel.Api.Requests.HotTours;
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

        /// <summary>
        /// Get actual hot-tours
        /// </summary>
        /// <param name="data">Request params</param>
        /// <returns>Hot-tours</returns>
        [HttpGet("get", Name = nameof(GetTours))]
        [ProducesResponseType(typeof(List<GetHotToursViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTours([FromQuery] GetHotToursDto data)
        {
            var query = Mapper.Map<GetHotToursQuery>(data);
            var result = await Mediator.Send(query);
            return FromResult(result);
        }
        
        /// <summary>
        /// Get all countries
        /// </summary>
        /// <param name="data">Request params</param>
        /// <returns>All countries</returns>
        [HttpGet("countries", Name = nameof(GetAllCountries))]
        [ProducesResponseType(typeof(List<GetCountriesViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCountries([FromQuery] GetCountriesDto data)
        {
            var query = Mapper.Map<GetCountriesQuery>(data);
            var result = await Mediator.Send(query);
            return FromResult(result);
        }

        /// <summary>
        /// Get all hotel categories
        /// </summary>
        /// <param name="data">Request params</param>
        /// <returns>All hotel categories</returns>
        [HttpGet("categories", Name = nameof(GetAllHotelCategories))]
        [ProducesResponseType(typeof(List<GetHotelCategoriesViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllHotelCategories([FromQuery] GetHotelCategoriesDto data)
        {
            var query = Mapper.Map<GetHotelCategoriesQuery>(data);
            var result = await Mediator.Send(query);
            return FromResult(result);
        }
        
        /// <summary>
        /// Get all departure locations
        /// </summary>
        /// <param name="data">Request params</param>
        /// <returns>All departures</returns>
        [HttpGet("departures", Name = nameof(GetAllDepartures))]
        [ProducesResponseType(typeof(List<GetDeparturesViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllDepartures([FromQuery] GetDeparturesDto data)
        {
            var query = Mapper.Map<GetDeparturesQuery>(data);
            var result = await Mediator.Send(query);
            return FromResult(result);
        }
    }
}