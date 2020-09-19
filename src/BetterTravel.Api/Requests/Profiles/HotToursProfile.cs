using AutoMapper;
using BetterTravel.Api.Requests.HotTours;
using BetterTravel.DataAccess.Abstractions.Enums;
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
            CreateMap<GetHotToursDto, GetHotToursQuery>().ConstructUsing(ConstructHotTours);
            CreateMap<GetCountriesDto, GetCountriesQuery>();
            CreateMap<GetDeparturesDto, GetDeparturesQuery>();
            CreateMap<GetHotelCategoriesDto, GetHotelCategoriesQuery>();
        }

        private static GetHotToursQuery ConstructHotTours(
            GetHotToursDto dto, ResolutionContext arg2) => 
            new GetHotToursQuery(
                dto.Countries ?? new int[0], 
                dto.Departures ?? new int[0], 
                dto.HotelCategories ?? new HotelCategoryType[0]);
    }
}