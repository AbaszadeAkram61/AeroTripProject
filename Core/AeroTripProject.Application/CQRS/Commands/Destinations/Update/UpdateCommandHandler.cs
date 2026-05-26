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

namespace AeroTripProject.Application.CQRS.Commands.Destinations.Update
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommandRequest, List<UpdateCommandResponse>>
    {
        private readonly IValidator<Destination> _validator;
        private readonly IRepostory<Destination> _repostory;

        public UpdateCommandHandler(IValidator<Destination> validator)
        {
            _validator = validator;
        }

        public async Task<List<UpdateCommandResponse>> Handle(UpdateCommandRequest request, CancellationToken cancellationToken)
        {
            var destination = new Destination
            {
                Id = request.Id,
                City = request.City,
                DayNight = request.DayNight,
                Price = request.Price??0,
                Image = request.Image,
                CoverImage = request.CoverImage,
                Description = request.Description,
                Capacity = request.Capacity??0,
                Details1 = request.Details1,
                Details2 = request.Details2,
                Image2 = request.Image2
                
            };


            var validationresult = _validator.Validate(destination);
            if (!validationresult.IsValid)
            {
              return validationresult.Errors.Select(x => new UpdateCommandResponse
                {
                    Propertyname = x.PropertyName,
                    Errormessage = x.ErrorMessage,
                    Success = false

                }).ToList();
               
            }
            await _repostory.UpdateAsync(destination);

            return new List<UpdateCommandResponse>
            {
                  new UpdateCommandResponse
            {
                Message = "Melumat deyisdirildi",
                Success=true
            }
            };
           
        }
    }
}
