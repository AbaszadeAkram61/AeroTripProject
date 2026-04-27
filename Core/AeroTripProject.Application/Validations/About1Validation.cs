using AeroTripProject.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.Validations
{
    public class About1Validation:AbstractValidator<About1>
    {
        public About1Validation()
        {
            RuleFor(x => x.Title1).NotEmpty().NotNull().WithMessage("Title1 bos ola bilmez");
            RuleFor(x => x.Description1).NotEmpty().NotNull().WithMessage("Description1 bos ola bilmez")
                .MinimumLength(7).WithMessage("Description1 in  minumum uzunlugu 7 dir ")
                .MaximumLength(200).WithMessage("Description1 in maxiumum uzunlugu 200 dur");

            RuleFor(x => x.Image1).NotNull().NotEmpty().WithMessage("Image1 bos ola bilmez");
            RuleFor(x => x.Title2).NotEmpty().NotNull().WithMessage("Title2 bos ola bilmez");

            RuleFor(x => x.Description2).NotEmpty().NotNull().WithMessage("Description2 bos ola bilmez")
                .MinimumLength(7).WithMessage("Description2 in  minumum uzunlugu 7 dir ")
                .MaximumLength(200).WithMessage("Description2 in maxiumum uzunlugu 200 dur");
        }
    }
}
