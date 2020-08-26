using System.Collections.Generic;
using BetterTravel.MediatR.Core.Abstractions;
using BetterTravel.Queries.ViewModels;

namespace BetterTravel.Queries.HotTours.GetHotelCategories
{
    public class GetHotelCategoriesQuery : IQuery<List<GetHotelCategoriesViewModel>>
    {
        public bool Localize { get; set; }
    }
}