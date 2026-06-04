using AeroTripProject.Domain.Entities.Identity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.Validations
{
    public class AppRoleValidation:AbstractValidator<AppRole>
    {
        public AppRoleValidation()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Rol adı boş ola bilməz")
            .MinimumLength(3).WithMessage("Rol adı minimum 3 simvol olmalıdır")
            .MaximumLength(50).WithMessage("Rol adı maksimum 50 simvol ola bilər");
        }
    }
}
