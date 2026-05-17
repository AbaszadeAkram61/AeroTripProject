using AeroTripProject.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.Validations
{
    public class CommentValidation:AbstractValidator<Comment>
    {
        public CommentValidation()
        {
     

            RuleFor(x => x.CommentContent)
         .NotEmpty().WithMessage("Şərh boş ola bilməz")
         .MinimumLength(5).WithMessage("Şərh minimum 5 simvol olmalıdır")
         .MaximumLength(500).WithMessage("Şərh maksimum 500 simvol ola bilər");


            RuleFor(x => x.CommentDate)
        .NotEmpty().WithMessage("Tarix boş ola bilməz")
        .LessThanOrEqualTo(DateTime.Now)
        .WithMessage("Tarix gələcəkdə ola bilməz");

        }
    }
}
