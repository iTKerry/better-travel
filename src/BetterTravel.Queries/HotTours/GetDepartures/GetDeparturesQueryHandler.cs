﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetterTravel.Common.Localization;
using BetterTravel.DataAccess.Abstraction.Entities;
using BetterTravel.MediatR.Core.HandlerResults.Abstractions;
using BetterTravel.Queries.Abstractions;
using BetterTravel.Queries.ViewModels;

namespace BetterTravel.Queries.HotTours.GetDepartures
{
    public class GetDeparturesQueryHandler 
        : QueryHandlerBase<GetDeparturesQuery, List<GetDeparturesViewModel>>
    {
        public override Task<IHandlerResult<List<GetDeparturesViewModel>>> Handle(
            GetDeparturesQuery request, 
            CancellationToken cancellationToken)
        {
            var departures = DepartureLocation.AllDepartures
                .Select(departureLocation => new GetDeparturesViewModel
                {
                    Id = departureLocation.Id,
                    Name = request.Localize
                        ? L.GetValue(departureLocation.Name, Culture.Ru)
                        : departureLocation.Name
                })
                .ToList();
            
            return Task.FromResult(Ok(departures));
        }
    }
}