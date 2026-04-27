using AeroTripProject.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.Validations
{
    public class About2Validation:AbstractValidator<About2>
    {
        public About2Validation()
        {
            RuleFor(x => x.Title1).NotEmpty().NotNull().WithMessage("Title1 bos ola bilmez");
            RuleFor(x => x.Title2).NotEmpty().NotNull().WithMessage("Title2 bos ola bilmez");

            RuleFor(x => x.Description).NotEmpty().NotNull().WithMessage("Description bos ola bilmez")
              .MinimumLength(7).WithMessage("Description in  minumum uzunlugu 7 dir ")
              .MaximumLength(200).WithMessage("Description in maxiumum uzunlugu 200 dur");

            RuleFor(x => x.Image).NotNull().NotEmpty().WithMessage("Image bos ola bilmez");
        }
    }
}
