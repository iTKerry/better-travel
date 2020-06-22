using AutoMapper;
using BetterTravel.Api.Requests.HotTours;
using BetterTravel.Queries.HotTours;

namespace BetterTravel.Api.Requests.Mappings
{
    public class HotToursProfile : Profile
    {
        public HotToursProfile()
        {
            CreateMap<GetHotToursDto, GetHotToursQuery>();
        }
    }
}