using AeroTripProject.Application.Repostories;
using AeroTripProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.CQRS.Queries.Guides.ChangeStatus
{
    public class ChangeStatusQueryHandler : IRequestHandler<ChangeStatusQueryRequest, ChangeStatusQueryResponse>
    {
        private readonly IRepostory<Guide> _repostory;

        public ChangeStatusQueryHandler(IRepostory<Guide> repostory)
        {
            _repostory = repostory;
        }

        public async Task<ChangeStatusQueryResponse> Handle(ChangeStatusQueryRequest request, CancellationToken cancellationToken)
        {
           var guide= await _repostory.ChangeStatusAsync(request.Id, request.Status);

            return new ChangeStatusQueryResponse
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
