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
            RuleFor(x => x.Name)
     .NotEmpty().WithMessage("Ad boş ola bilməz");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıqlama boş ola bilməz")
                .MinimumLength(7).WithMessage("Açıqlamanın minimum uzunluğu 7 simvol olmalıdır")
                .MaximumLength(200).WithMessage("Açıqlamanın maksimum uzunluğu 200 simvol olmalıdır");

            RuleFor(x => x.Image)
                .NotEmpty().WithMessage("Şəkil boş ola bilməz");

            RuleFor(x => x.TiktokUrl)
                .NotEmpty().WithMessage("TikTok linki boş ola bilməz");

            RuleFor(x => x.InstagramUrl)
                .NotEmpty().WithMessage("Instagram linki boş ola bilməz");
        }
    }
}
