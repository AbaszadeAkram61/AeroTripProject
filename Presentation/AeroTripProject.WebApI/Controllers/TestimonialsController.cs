using AeroTripProject.Application.Dtos.Testimonail;

using AeroTripProject.Application.Repostories;
using AeroTripProject.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.WebApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialsController : ControllerBase
    {
        private readonly IRepostory<Testimonial> _repostory;
        private readonly IValidator<Testimonial> _validator;

        public TestimonialsController(IRepostory<Testimonial> repostory, IValidator<Testimonial> validator)
        {
            _repostory = repostory;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _repostory.GetListAsync());

        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var Testimonial = await _repostory.GetByIdAsync(Id);
            return Ok(Testimonial);

        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTestimonial createTestimonial)
        {
            var Testimonial = new Testimonial
            {
               Client=createTestimonial.Client,
               Comment=createTestimonial.Comment,
               ClientImage=createTestimonial.ClientImage,
               Status=createTestimonial.Status
            };
            var validationresult = _validator.Validate(Testimonial);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(e => e.ErrorMessage));
            }
            await _repostory.InsertAsync(Testimonial);
            return Ok("Melumat elave olundu");
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateTestimonial updateTestimonial)
        {
            var Testimonial = new Testimonial
            {
               Id=updateTestimonial.Id,
               Client=updateTestimonial.Client,
               Comment=updateTestimonial.Comment,
               ClientImage=updateTestimonial.ClientImage,
               Status=updateTestimonial.Status
               
            };


            var validationresult = _validator.Validate(Testimonial);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(e => e.ErrorMessage));
            }
            await _repostory.UpdateAsync(Testimonial);
            return Ok("Melumat deyisdirildi");
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            await _repostory.DeleteAsync(Id);
            return Ok("Melumat silindi");
        }

        [HttpGet("Count")]
        public async Task<IActionResult> GetCount()
        {
            return Ok(await _repostory.CountAsync());

        }
    }
}
