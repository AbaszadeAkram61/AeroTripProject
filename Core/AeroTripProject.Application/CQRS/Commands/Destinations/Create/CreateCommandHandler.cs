using AeroTripProject.Application.Dtos.Destination;
using AeroTripProject.Application.Repostories;
using AeroTripProject.Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.CQRS.Commands.Destinations.Create
{
    public class CreateCommandHandler : IRequestHandler<CreateCommandRequest, List<CreateCommandResponse>>
    {
        private readonly IRepostory<Destination> _repostory;
        private readonly IValidator<Destination> _validator;

        public CreateCommandHandler(IRepostory<Destination> repostory, IValidator<Destination> validator)
        {
            _repostory = repostory;
            _validator = validator;
        }

        public async Task<List<CreateCommandResponse>> Handle(CreateCommandRequest request, CancellationToken cancellationToken)
        {
            var destination = new Destination
            {
                City = request.City,
                DayNight = request.DayNight,
                Price = request.Price ?? 0,
                Image = request.Image,
                CoverImage = request.CoverImage,
                Description = request.Description,
                Capacity = request.Capacity ?? 0,
                Details1 = request.Details1,
                Details2 = request.Details2,
                Image2 = request.Image2
               
            };
            var validationresult = _validator.Validate(destination);
            if (!validationresult.IsValid)
            {
                return validationresult.Errors.Select(x => new CreateCommandResponse
                {
                    Propertyname=x.PropertyName,
                    Errormessage = x.ErrorMessage,
                    IsSuccess = false
                }).ToList();

            }
            await _repostory.InsertAsync(destination);

            return new List<CreateCommandResponse>
    {
                new CreateCommandResponse
                {
                    Message = "Melumat elave olundu",
                    IsSuccess = true
                }
    };
        }
    }
}
