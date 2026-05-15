using AeroTripProject.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.Validations
{
    public class DestinationValidation:AbstractValidator<Destination>
    {
        public DestinationValidation()
        {
            RuleFor(x => x.City).NotEmpty().NotNull().WithMessage("City bos ola bilmez");
            RuleFor(x => x.DayNight).NotEmpty().NotNull().WithMessage("DayNight bos ola bilmez");

            RuleFor(x => x.Price).NotNull().NotEmpty().WithMessage("Price bos ola bilmez")
                .GreaterThan(0).WithMessage("Price Negative ola bilmez")
                .LessThan(5000).WithMessage("Price 5000 den boyuk ola bilmez");

            RuleFor(x => x.Image).NotNull().NotEmpty().WithMessage("Image bos ola bilmez");

            RuleFor(x => x.Description).NotEmpty().NotNull().WithMessage("Description bos ola bilmez")
         .MinimumLength(7).WithMessage("Description in  minumum uzunlugu 7 dir ")
         .MaximumLength(1000).WithMessage("Description in maxiumum uzunlugu 1000 dur");

            RuleFor(x => x.Capacity).NotNull().NotEmpty().WithMessage("Capacity bos ola bilmez");
        }
    }
}
