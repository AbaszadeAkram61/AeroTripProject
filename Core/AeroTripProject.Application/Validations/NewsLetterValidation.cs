using AeroTripProject.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.Validations
{
   public class NewsLetterValidation:AbstractValidator<NewsLetter>
    {
        public NewsLetterValidation()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("Email bos ola bilmez");
        }
    }
}
