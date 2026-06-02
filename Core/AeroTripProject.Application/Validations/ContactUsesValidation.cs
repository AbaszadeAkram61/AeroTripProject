using AeroTripProject.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.Validations
{
    public  class ContactUsesValidation:AbstractValidator<ContactUs>
    {
        public ContactUsesValidation()
        {
       
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ad boş ola bilməz")
                .MinimumLength(3).WithMessage("Ad minimum 3 simvol olmalıdır");

            RuleFor(x => x.Mail)
      .NotEmpty().WithMessage("Email boş ola bilməz")
      .EmailAddress().WithMessage("Düzgün email daxil edin (məs: example@gmail.com)");

            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Mövzu boş ola bilməz")
                .MinimumLength(3).WithMessage("Mövzu minimum 3 simvol olmalıdır");

            RuleFor(x => x.MessageBody)
                .NotEmpty().WithMessage("Mesaj boş ola bilməz")
                .MinimumLength(10).WithMessage("Mesaj minimum 10 simvol olmalıdır");

           
        }
    }
}
