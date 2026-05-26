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

namespace AeroTripProject.Application.CQRS.Commands.Guides.Create
{
    public class CreateCommandHandler : IRequestHandler<CreateCommandRequest, List<CreateCommandResponse>>
    {
        private readonly IValidator<Guide> _validator;
        private readonly IRepostory<Guide> _repostory;

        public CreateCommandHandler(IValidator<Guide> validator, IRepostory<Guide> repostory)
        {
            _validator = validator;
            _repostory = repostory;
        }

        public async Task<List<CreateCommandResponse>> Handle(CreateCommandRequest request, CancellationToken cancellationToken)
        {
            var guide = new Guide
            {
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
               return validationresult.Errors.Select(x => new CreateCommandResponse
                {
                    PropertyName = x.PropertyName,
                    ErrorMessage = x.ErrorMessage,
                    Success=false
                }).ToList();
                
            }

            await _repostory.InsertAsync(guide);
            return new List<CreateCommandResponse>{
                new CreateCommandResponse
                {
                    Message="Melumat elave olundu",
                    Success=true

                }
            };
        }
    }
}
