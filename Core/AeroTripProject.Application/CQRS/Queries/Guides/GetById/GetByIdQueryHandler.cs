using AeroTripProject.Application.Repostories;
using AeroTripProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.CQRS.Queries.Guides.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQueryRequest, GetByIdQueryResponse>
    {
        private readonly IRepostory<Guide> _repostory;

        public GetByIdQueryHandler(IRepostory<Guide> repostory)
        {
            _repostory = repostory;
        }

        public async Task<GetByIdQueryResponse> Handle(GetByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var guide = await _repostory.GetByIdAsync(request.Id);
            return new GetByIdQueryResponse
            {
                Id = guide.Id,
                Name = guide.Name,
                Description = guide.Description,
                Image = guide.Image,
                TiktokUrl = guide.TiktokUrl,
                InstagramUrl = guide.InstagramUrl,
                Status=guide.Status
            };
            
        }
    }
}
