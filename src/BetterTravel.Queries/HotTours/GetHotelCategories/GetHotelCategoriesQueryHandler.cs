using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Common.Localization;
using BetterTravel.DataAccess.Abstractions.Enums;
using BetterTravel.MediatR.Core.Abstractions;
using BetterTravel.Queries.Abstractions;

namespace BetterTravel.Queries.HotTours.GetHotelCategories
{
    public class GetHotelCategoriesQueryHandler 
        : QueryHandlerBase<GetHotelCategoriesQuery, List<GetHotelCategoriesViewModel>>
    {
        public override Task<IHandlerResult<List<GetHotelCategoriesViewModel>>> Handle(
            GetHotelCategoriesQuery request, 
            CancellationToken cancellationToken)
        {
            var categories = Enum
                .GetValues(typeof(HotelCategoryType))
                .Cast<HotelCategoryType>()
                .Select(category => new GetHotelCategoriesViewModel
                {
                    Id = (int) category,
                    Name = request.Localize
                        ? L.GetValue(category.ToString(), Culture.Ru)
                        : category.ToString()
                })
                .ToList();

            return Task.FromResult(Data(categories));
        }
    }
}