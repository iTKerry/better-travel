using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Api.Queries.Abstractions;
using BetterTravel.Api.Queries.ViewModels;
using BetterTravel.Common.Localization;
using BetterTravel.Domain.Entities;
using BetterTravel.MediatR.Core.Abstractions;

namespace BetterTravel.Api.Queries.HotTours.GetHotelCategories
{
    public class GetHotelCategoriesQueryHandler 
        : ApiQueryHandler<GetHotelCategoriesQuery, List<GetHotelCategoriesViewModel>>
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