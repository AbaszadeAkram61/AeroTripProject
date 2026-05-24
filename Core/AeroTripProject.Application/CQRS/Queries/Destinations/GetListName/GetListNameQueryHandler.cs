using AeroTripProject.Application.Repostories;
using AeroTripProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.CQRS.Queries.Destinations.GetListName
{
    public class GetListNameQueryHandler : IRequestHandler<GetListNameQueryRequest, List<GetListNameQueryResponse>>
    {
        private readonly IRepostory<Destination> _repostory;

        public GetListNameQueryHandler(IRepostory<Destination> repostory)
        {
            _repostory = repostory;
        }

        public async Task<List<GetListNameQueryResponse>> Handle(GetListNameQueryRequest request, CancellationToken cancellationToken)
        {
           var cityies= await _repostory.GetListNameAsync(x => x.City);
            return cityies.Select(x => new GetListNameQueryResponse
            {
                City = x
            }).ToList();

        }
    }
}
