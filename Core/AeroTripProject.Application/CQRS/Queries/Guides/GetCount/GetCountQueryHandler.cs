using AeroTripProject.Application.Repostories;
using AeroTripProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.CQRS.Queries.Guides.GetCount
{
    public class GetCountQueryHandler : IRequestHandler<GetCountQueryRequest, GetCountQueryResponse>
    {
        private readonly IRepostory<Guide> _repostory;

        public GetCountQueryHandler(IRepostory<Guide> repostory)
        {
            _repostory = repostory;
        }

        public async Task<GetCountQueryResponse> Handle(GetCountQueryRequest request, CancellationToken cancellationToken)
        {
           int var = await _repostory.CountAsync();
            return new GetCountQueryResponse
            {
                Count = var
            };
        }
    }
}
