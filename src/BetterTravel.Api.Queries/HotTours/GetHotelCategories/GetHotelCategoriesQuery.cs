using System.Collections.Generic;
using BetterTravel.Api.Queries.ViewModels;
using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.Api.Queries.HotTours.GetHotelCategories
{
    public class GetHotelCategoriesQuery : IQuery<List<GetHotelCategoriesViewModel>>
    {
        public bool Localize { get; set; }
    }
}