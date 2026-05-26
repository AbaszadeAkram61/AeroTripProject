using AeroTripProject.Application.Dtos.Guide;
using AeroTripProject.Application.Repostories;
using AeroTripProject.Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.CQRS.Commands.Guides.Update
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommandRequest, List<UpdateCommandResponse>>
    {
        private readonly IValidator<Guide> _validator;
        private readonly IRepostory<Guide> _repostory;

        public UpdateCommandHandler(IValidator<Guide> validator, IRepostory<Guide> repostory)
        {
            _validator = validator;
            _repostory = repostory;
        }

        public async Task<List<UpdateCommandResponse>> Handle(UpdateCommandRequest request, CancellationToken cancellationToken)
        {
            var guide = new Guide
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                Image = request.Image,
                TiktokUrl = request.TiktokUrl,
                InstagramUrl = request.InstagramUrl,
                Status = request.Status
            };


            var validationresult = _validator.Validate(guide);
            if (!validationresult.IsValid)
            {
                return validationresult.Errors.Select(x => new UpdateCommandResponse
                {
                    Propertyname=x.PropertyName,
                    Errormessage=x.ErrorMessage,
                    Success=false
                }).ToList();
            }
            await _repostory.UpdateAsync(guide);
            return new List<UpdateCommandResponse>
            {
                new UpdateCommandResponse
                {
                    Message="Melumat deyisdirildi",
                    Success=true
                }
            };
        }
    }
}
