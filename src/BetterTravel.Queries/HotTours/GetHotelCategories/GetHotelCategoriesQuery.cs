using System.Collections.Generic;
using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.Queries.HotTours.GetHotelCategories
{
    public class GetHotelCategoriesQuery : IQuery<List<GetHotelCategoriesViewModel>>
    {
        public bool Localize { get; set; }
    }
}