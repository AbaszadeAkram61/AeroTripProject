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
            RuleFor(x => x.Description).NotEmpty().NotNull().WithMessage("Description bos ola bilmez")
             .MinimumLength(7).WithMessage("Description in  minumum uzunlugu 7 dir ")
             .MaximumLength(200).WithMessage("Description in maxiumum uzunlugu 200 dur");

            RuleFor(x => x.Mail).NotEmpty().NotNull().WithMessage("Mail bos ola bilmez");
            RuleFor(x => x.Address).NotNull().NotEmpty().WithMessage("Address bos ola bilmez");
            RuleFor(x => x.Phone).NotEmpty().NotNull().WithMessage("Phone bos ola bilmez");
            RuleFor(x => x.MapLocation).NotNull().NotEmpty().WithMessage("MapLocation bos ola bilmez");
        }
    }
}
