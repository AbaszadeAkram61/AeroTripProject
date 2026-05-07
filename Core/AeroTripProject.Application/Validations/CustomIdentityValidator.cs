using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.Validations
{
    public class CustomIdentityValidator : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError()
            {
                Code = "DuplicateUserName",
                Description = $"'{userName}' istifadəçi adı artıq mövcuddur"
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError()
            {
                Description = "Şifrədə ən az 1 böyük hərf olmalıdır"
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError()
            {
                Description = "Şifrədə ən az 1 kiçik hərf olmalıdır"
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError()
            {
                Description = "Şifrədə ən az 1 rəqəm olmalıdır"
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError()
            {
                Description = "Şifrədə ən az 1 xüsusi simvol olmalıdır"
            };
        }
    }
}
