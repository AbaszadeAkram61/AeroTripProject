using AeroTripProject.Application.Dtos.Mail;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.Validations
{
    public class MailValidation : AbstractValidator<MailRequest>
    {
        public MailValidation()
        {
            RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Ad boş ola bilməz");

            RuleFor(x => x.ReceiverMail)
                .NotEmpty().WithMessage("Email boş ola bilməz")
                .EmailAddress().WithMessage("Düzgün email daxil edin. Məsələn: example@gmail.com");

            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Mövzu boş ola bilməz");

            RuleFor(x => x.Body)
                .NotEmpty().WithMessage("Mesaj boş ola bilməz")
                .MinimumLength(10).WithMessage("Mesaj minimum 10 simvol olmalıdır");
        }
    }
}
