using AeroTripProject.Application.Validations;
using AeroTripProject.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application
{
   public static class ServiceRegistartion
    { 
        public static void AddApplicationRegistartion(this IServiceCollection services)
        {
            services.AddScoped<IValidator<About1>, About1Validation>();
            services.AddScoped<IValidator<About2>, About2Validation>();
            services.AddScoped<IValidator<Contact>, ContactValidation>();
            services.AddScoped<IValidator<Destination>, DestinationValidation>();
           
            services.AddScoped<IValidator<Feature>, FeatureValidation>();
            services.AddScoped<IValidator<Guide>, GuideValidation>();
            services.AddScoped<IValidator<NewsLetter>, NewsLetterValidation>();
            services.AddScoped<IValidator<SubAbout>, SubAboutValidation>();
            services.AddScoped<IValidator<Testimonial>, TestimonialValidation>();
            services.AddScoped<IValidator<Comment>, CommentValidation>();
        }
    }
}
