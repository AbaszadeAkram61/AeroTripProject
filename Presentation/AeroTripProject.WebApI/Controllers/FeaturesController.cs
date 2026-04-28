using AeroTripProject.Application.Dtos.Destination;
using AeroTripProject.Application.Dtos.Feature;
using AeroTripProject.Application.Repostories;
using AeroTripProject.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.WebApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IRepostory<Feature> _repostory;
        private readonly IValidator<Feature> _validator;

        public FeaturesController(IRepostory<Feature> repostory, IValidator<Feature> validator)
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
            var feature = await _repostory.GetByIdAsync(Id);
            return Ok(feature);

        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateFeature createFeature)
        {
            var feature = new Feature
            {
              Title=createFeature.Title,
              Description=createFeature.Description,
              Image=createFeature.Image,
              Status=createFeature.Status
            };
            var validationresult = _validator.Validate(feature);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(e => e.ErrorMessage));
            }
            await _repostory.InsertAsync(feature);
            return Ok("Melumat elave olundu");
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateFeature updateFeature)
        {
            var feature = new Feature
            {
                Title = updateFeature.Title,
                Description = updateFeature.Description,
                Image = updateFeature.Image,
                Status = updateFeature.Status
            };


            var validationresult = _validator.Validate(feature);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(e => e.ErrorMessage));
            }
            await _repostory.UpdateAsync(feature);
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
