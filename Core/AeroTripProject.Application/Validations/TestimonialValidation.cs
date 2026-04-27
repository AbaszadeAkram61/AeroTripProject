using AeroTripProject.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.Validations
{
    public class TestimonialValidation:AbstractValidator<Testimonial>
    {
        public TestimonialValidation()
        {
            RuleFor(x => x.Client).NotNull().NotEmpty().WithMessage("Client bos ola bilmez");

            RuleFor(x => x.Comment).NotNull().NotEmpty().WithMessage("Comment bos ola bilmez");

            RuleFor(x => x.ClientImage).NotNull().NotEmpty().WithMessage("ClientImage bos ola bilmez");


        }
    }
}
