using AeroTripProject.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.Validations
{
    public class GuideValidation:AbstractValidator<Guide>
    {
        public GuideValidation()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name bos ola bilmez");

            RuleFor(x => x.Description).NotEmpty().NotNull().WithMessage("Description bos ola bilmez")
      .MinimumLength(7).WithMessage("Description in  minumum uzunlugu 7 dir ")
      .MaximumLength(200).WithMessage("Description in maxiumum uzunlugu 200 dur");

            RuleFor(x => x.Image).NotNull().NotEmpty().WithMessage("Image bos ola bilmez");

            RuleFor(x => x.TiktokUrl).NotNull().NotEmpty().WithMessage("TiktokUrl bos ola bilmez");

            RuleFor(x => x.InstagramUrl).NotNull().NotEmpty().WithMessage("InstagramUrl bos ola bilmez");
        }
    }
}
