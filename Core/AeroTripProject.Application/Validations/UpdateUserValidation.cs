using AeroTripProject.Application.Dtos.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.Validations
{
    public class UpdateUserValidation : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidation()
        {
            RuleFor(x => x.NameSurname)
                .NotEmpty().WithMessage("Ad Soyad boş ola bilməz")
                .MinimumLength(3).WithMessage("Ad Soyad minimum 3 simvol olmalıdır");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş ola bilməz")
                .EmailAddress()
                .WithMessage("Email formatı belə olmalıdır: example@gmail.com");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("İstifadəçi adı boş ola bilməz")
                .MinimumLength(3).WithMessage("İstifadəçi adı minimum 3 simvol olmalıdır");

            RuleFor(x => x.Password)
                .MinimumLength(6).WithMessage("Şifrə minimum 6 simvol olmalıdır")
                .Matches("[A-Z]").WithMessage("Şifrədə ən az 1 böyük hərf olmalıdır")
                .Matches("[a-z]").WithMessage("Şifrədə ən az 1 kiçik hərf olmalıdır")
                .Matches("[0-9]").WithMessage("Şifrədə ən az 1 rəqəm olmalıdır")
                .When(x => !string.IsNullOrWhiteSpace(x.Password));

            RuleFor(x => x.PasswordConfirm)
                .Equal(x => x.Password)
                .WithMessage("Şifrələr eyni deyil")
                .When(x => !string.IsNullOrWhiteSpace(x.Password));
        }
    }
}
