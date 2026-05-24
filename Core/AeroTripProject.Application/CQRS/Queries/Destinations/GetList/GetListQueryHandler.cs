using AeroTripProject.Application.Repostories;
using AeroTripProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.CQRS.Queries.Destinations.GetList
{
    public class GetListQueryHandler : IRequestHandler<GetListQueryRequest, List<GetListQueryResponse>>
    {
        private readonly IRepostory<Destination> _repostory;

        public GetListQueryHandler(IRepostory<Destination> repostory)
        {
            _repostory = repostory;
        }

        public async Task<List<GetListQueryResponse>> Handle(GetListQueryRequest request, CancellationToken cancellationToken)
        {
            var values = await _repostory.GetListAsync();
            return values.Select(x => new GetListQueryResponse
            {
                Id = x.Id,
                City = x.City,
                DayNight = x.DayNight,
                Price = x.Price,
                Image = x.Image,
                Description = x.Description,
                Capacity = x.Capacity,

                CoverImage = x.CoverImage,
                Details1 = x.Details1,
                Details2 = x.Details2,
                Image2 = x.Image2,

                
            }).ToList();
        }
    }
}
