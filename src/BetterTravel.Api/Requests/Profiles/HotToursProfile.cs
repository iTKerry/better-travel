using AutoMapper;
using BetterTravel.Api.Requests.HotTours;
using BetterTravel.Queries.HotTours.GetCountries;
using BetterTravel.Queries.HotTours.GetDepartures;
using BetterTravel.Queries.HotTours.GetHotelCategories;
using BetterTravel.Queries.HotTours.GetHotTours;

namespace BetterTravel.Api.Requests.Profiles
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