using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Common.Localization;
using BetterTravel.Domain.Entities;
using BetterTravel.MediatR.Core.HandlerResults.Abstractions;
using BetterTravel.Queries.Abstractions;
using BetterTravel.Queries.ViewModels;

namespace BetterTravel.Queries.HotTours.GetHotelCategories
{
    public class GetHotelCategoriesQueryHandler 
        : QueryHandlerBase<GetHotelCategoriesQuery, List<GetHotelCategoriesViewModel>>
    {
        public override Task<IHandlerResult<List<GetHotelCategoriesViewModel>>> Handle(
            GetHotelCategoriesQuery request, 
            CancellationToken cancellationToken)
        {
            var categories = HotelCategory.AllCategories
                .Select(category => new GetHotelCategoriesViewModel
                {
                    Id = category.Id,
                    Name = request.Localize
                        ? L.GetValue(category.Name, Culture.Ru)
                        : category.Name
                })
                .ToList();

            return Task.FromResult(Ok(categories));
        }
    }
}