using AeroTripProject.Application.CQRS.Commands.Destinations.Create;
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
            RuleFor(x => x.City)
      .NotEmpty().WithMessage("Şəhər boş ola bilməz");

            RuleFor(x => x.DayNight)
                .NotEmpty().WithMessage("Gün və gecə məlumatı boş ola bilməz");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Qiymət boş ola bilməz")
                .GreaterThan(0).WithMessage("Qiymət mənfi ola bilməz")
                .LessThan(5000).WithMessage("Qiymət 5000-dən böyük ola bilməz");

            RuleFor(x => x.Image)
                .NotEmpty().WithMessage("Əsas Şəkil boş ola bilməz");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıqlama boş ola bilməz")
                .MinimumLength(7).WithMessage("Açıqlamanın minimum uzunluğu 7 simvol olmalıdır")
                .MaximumLength(1000).WithMessage("Açıqlamanın maksimum uzunluğu 1000 simvol olmalıdır");

            RuleFor(x => x.Capacity)
                .NotEmpty().WithMessage("Tutum boş ola bilməz");

            RuleFor(x => x.CoverImage)
                .NotEmpty().WithMessage("Örtük şəkli boş ola bilməz");

            RuleFor(x => x.Details1)
                .NotEmpty().WithMessage("Birinci Açıqlama məlumatı boş ola bilməz");

            RuleFor(x => x.Details2)
                .NotEmpty().WithMessage("İkinci Açıqlama məlumatı boş ola bilməz");

            RuleFor(x => x.Image2)
                .NotEmpty().WithMessage("Əlavə şəkil boş ola bilməz");
        }
    }
}
