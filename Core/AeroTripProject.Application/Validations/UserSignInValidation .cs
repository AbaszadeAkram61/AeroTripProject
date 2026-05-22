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
    public class UserSignInValidation : AbstractValidator<UserSignIn>
    {
        public UserSignInValidation()
        {
            RuleFor(x => x.Username)
            .NotEmpty().WithMessage("İstifadəçi adı boş ola bilməz");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifrə boş ola bilməz");
        }
    }
}
