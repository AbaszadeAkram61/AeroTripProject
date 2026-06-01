using AeroTripProject.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.Validations
{
    public class ContactValidation:AbstractValidator<Contact>
    {
        public ContactValidation()
        {
            RuleFor(x => x.Description)
      .NotEmpty().WithMessage("Açıqlama sahəsi boş ola bilməz")
      .NotNull().WithMessage("Açıqlama sahəsi boş ola bilməz")
      .MinimumLength(7).WithMessage("Açıqlama minimum 7 simvol olmalıdır")
      .MaximumLength(200).WithMessage("Açıqlama maksimum 200 simvol ola bilər");

            RuleFor(x => x.Mail)
                .NotEmpty().WithMessage("E-poçt ünvanı boş ola bilməz")
                .NotNull().WithMessage("E-poçt ünvanı boş ola bilməz");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Ünvan sahəsi boş ola bilməz")
                .NotNull().WithMessage("Ünvan sahəsi boş ola bilməz");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Telefon nömrəsi boş ola bilməz")
                .NotNull().WithMessage("Telefon nömrəsi boş ola bilməz");

            RuleFor(x => x.MapLocation)
                .NotEmpty().WithMessage("Xəritə ünvanı boş ola bilməz")
                .NotNull().WithMessage("Xəritə ünvanı boş ola bilməz");
        }
    }
}
