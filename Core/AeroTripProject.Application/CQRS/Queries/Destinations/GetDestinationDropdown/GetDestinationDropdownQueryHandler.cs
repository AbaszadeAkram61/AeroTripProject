using AeroTripProject.Application.Dtos.Destination;
using AeroTripProject.Application.Repostories;
using AeroTripProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.CQRS.Queries.Destinations.GetDestinationDropdown
{
    public class GetDestinationDropdownQueryHandler : IRequestHandler<GetDestinationDropdownQueryRequest, List<GetDestinationDropdownQueryResponse>>
    {
        private readonly IRepostory<Destination> _repostory;

        public GetDestinationDropdownQueryHandler(IRepostory<Destination> repostory)
        {
            _repostory = repostory;
        }

        public async Task<List<GetDestinationDropdownQueryResponse>> Handle(GetDestinationDropdownQueryRequest request, CancellationToken cancellationToken)
        {
            var values = await _repostory.GetSelectedListAsync(x => new ResultDestination
            {
                Id = x.Id,
                City = x.City
            });

           return values.Select(x => new GetDestinationDropdownQueryResponse
            {
                Id = x.Id,
                City = x.City
            }).ToList();
        }
    }
}
