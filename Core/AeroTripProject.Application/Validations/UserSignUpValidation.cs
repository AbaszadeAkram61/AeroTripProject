using AeroTripProject.Application.Dtos.User;
using AeroTripProject.Domain.Entities.Identity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.Validations
{
    public class UserSignUpValidation:AbstractValidator<UserSignUp>
    {
        public UserSignUpValidation()
        {
            RuleFor(x => x.NameSurname)
          .NotEmpty().WithMessage("Ad Soyad boş ola bilməz")
          .MinimumLength(3).WithMessage("Ad Soyad minimum 3 simvol olmalıdır");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş ola bilməz")
                .EmailAddress().WithMessage("Düzgün email daxil edin");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("İstifadəçi adı boş ola bilməz")
                .MinimumLength(3).WithMessage("İstifadəçi adı minimum 3 simvol olmalıdır");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifrə boş ola bilməz")
                .MinimumLength(6).WithMessage("Şifrə minimum 6 simvol olmalıdır")
                .Matches("[A-Z]").WithMessage("Şifrədə ən az 1 böyük hərf olmalıdır")
                .Matches("[a-z]").WithMessage("Şifrədə ən az 1 kiçik hərf olmalıdır")
                .Matches("[0-9]").WithMessage("Şifrədə ən az 1 rəqəm olmalıdır");

            RuleFor(x => x.PasswordConfirm)
                .Equal(x => x.Password)
                .WithMessage("Şifrələr eyni deyil");
        }
    }
}
