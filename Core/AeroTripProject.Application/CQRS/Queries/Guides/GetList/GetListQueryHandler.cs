using AeroTripProject.Application.Repostories;
using AeroTripProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.CQRS.Queries.Guides.GetList
{
    public class GetListQueryHandler : IRequestHandler<GetListQueryRequest, List<GetListQueryResponse>>
    {
        private readonly IRepostory<Guide> _repostory;

        public GetListQueryHandler(IRepostory<Guide> repostory)
        {
            _repostory = repostory;
        }

        public async Task<List<GetListQueryResponse>> Handle(GetListQueryRequest request, CancellationToken cancellationToken)
        {
           var guides= await _repostory.GetListAsync();
            return guides.Select(x => new GetListQueryResponse
            {
                Id=x.Id,
                Name=x.Name,
                Description=x.Description,
                Image=x.Image,
                TiktokUrl=x.TiktokUrl,
                InstagramUrl=x.InstagramUrl,
                Status=x.Status

            }).ToList();
        }
    }
}
