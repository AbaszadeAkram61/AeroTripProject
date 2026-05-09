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
            RuleFor(x => x.Destination)
            .NotEmpty().WithMessage("Təyinat yeri boş ola bilməz");

            RuleFor(x => x.PersonCount)
                .NotEmpty().WithMessage("Şəxs sayı boş ola bilməz");

            RuleFor(x => x.ReservationDate)
                .NotEmpty().WithMessage("Rezervasiya tarixi boş ola bilməz")
                .GreaterThan(DateTime.Now.Date)
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
