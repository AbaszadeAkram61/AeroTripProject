using AeroTripProject.Application.Repostories;
using AeroTripProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.CQRS.Queries.Destinations.GetCount
{
    public class GetCountQueryHandler : IRequestHandler<GetCountQueryRequest, GetCountQueryResponse>
    {
        private readonly IRepostory<Destination> _repostory;

        public GetCountQueryHandler(IRepostory<Destination> repostory)
        {
            _repostory = repostory;
        }

        public async Task<GetCountQueryResponse> Handle(GetCountQueryRequest request, CancellationToken cancellationToken)
        {
           var value= await _repostory.CountAsync();

            return new GetCountQueryResponse
            {
                Count = value
            };
        }
    }
}
