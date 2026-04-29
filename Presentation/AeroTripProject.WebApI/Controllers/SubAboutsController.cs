using AeroTripProject.Application.Dtos.SubAbout;
using AeroTripProject.Application.Repostories;
using AeroTripProject.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.WebApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubAboutsController : ControllerBase
    {
        private readonly IRepostory<SubAbout> _repostory;
        private readonly IValidator<SubAbout> _validator;

        public SubAboutsController(IRepostory<SubAbout> repostory, IValidator<SubAbout> validator)
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
            var SubAbout = await _repostory.GetByIdAsync(Id);
            return Ok(SubAbout);

        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSubAbout createSubAbout)
        {
            var SubAbout = new SubAbout
            {
               Title=createSubAbout.Title,
               Description=createSubAbout.Description
            };
            var validationresult = _validator.Validate(SubAbout);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(e => e.ErrorMessage));
            }
            await _repostory.InsertAsync(SubAbout);
            return Ok("Melumat elave olundu");
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateSubAbout updateSubAbout)
        {
            var SubAbout = new SubAbout
            {
               Id=updateSubAbout.Id,
               Title=updateSubAbout.Title,
               Description=updateSubAbout.Description
            };


            var validationresult = _validator.Validate(SubAbout);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(e => e.ErrorMessage));
            }
            await _repostory.UpdateAsync(SubAbout);
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
