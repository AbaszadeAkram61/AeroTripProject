using AeroTripProject.Application.Repostories;
using AeroTripProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.CQRS.Queries.Destinations.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQueryRequest, GetByIdQueryResponse>
    {
        private readonly IRepostory<Destination> _repostory;

        public GetByIdQueryHandler(IRepostory<Destination> repostory)
        {
            _repostory = repostory;
        }

        public async Task<GetByIdQueryResponse> Handle(GetByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var destination = await _repostory.GetByIdAsync(request.Id);

            return new GetByIdQueryResponse
            {
                Id = destination.Id,
                City = destination.City,
                DayNight = destination.DayNight,
                Price = destination.Price,
                Image = destination.Image,
                Description = destination.Description,
                Capacity = destination.Capacity,

                CoverImage = destination.CoverImage,
                Details1 = destination.Details1,
                Details2 = destination.Details2,
                Image2 = destination.Image2,
            };
        }
    }
}
