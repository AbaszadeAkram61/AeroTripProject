using AeroTripProject.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.Validations
{
    public class ReservationValidation:AbstractValidator<Reservation>
    {
        public ReservationValidation()
        {
            RuleFor(x => x.DestinationId)
    .GreaterThan(0)
    .WithMessage("İstiqamət seçilməlidir");

            RuleFor(x => x.PersonCount)
              .NotEmpty().WithMessage("Şəxs sayı boş ola bilməz")
              .GreaterThan(0).WithMessage("Şəxs sayı 0-dan böyük olmalıdır");

            RuleFor(x => x.ReservationDate)
       .NotEmpty().WithMessage("Rezervasiya tarixi boş ola bilməz")
       .Must(x => x.Date > DateTime.Now.Date)
       .WithMessage("Rezervasiya tarixi bugündən böyük olmalıdır");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıqlama boş ola bilməz")
                .MinimumLength(10)
                .WithMessage("Açıqlama minimum 5 simvol olmalıdır")
                .MaximumLength(300)
                .WithMessage("Açıqlama maksimum 300 simvol ola bilər");
        }
    }
}
