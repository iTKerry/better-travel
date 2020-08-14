using AutoMapper;
using BetterTravel.Api.Queries.HotTours.GetCountries;
using BetterTravel.Api.Queries.HotTours.GetDepartures;
using BetterTravel.Api.Queries.HotTours.GetHotelCategories;
using BetterTravel.Api.Queries.HotTours.GetHotTours;
using BetterTravel.Api.Requests.HotTours;

namespace BetterTravel.Api.Requests.Mappings
{
    public class HotToursProfile : Profile
    {
        public HotToursProfile()
        {
            CreateMap<GetHotToursDto, GetHotToursQuery>();
            CreateMap<GetCountriesDto, GetCountriesQuery>();
            CreateMap<GetDeparturesDto, GetDeparturesQuery>();
            CreateMap<GetHotelCategoriesDto, GetHotelCategoriesQuery>();
        }
    }
}