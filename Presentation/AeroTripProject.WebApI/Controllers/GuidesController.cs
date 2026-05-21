using AeroTripProject.Application.Dtos.Destination;
using AeroTripProject.Application.Dtos.Guide;
using AeroTripProject.Application.Repostories;
using AeroTripProject.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AeroTripProject.WebApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuidesController : ControllerBase
    {
        private readonly IRepostory<Guide> _repostory;
        private readonly IValidator<Guide> _validator;

        public GuidesController(IRepostory<Guide> repostory, IValidator<Guide> validator)
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
            var guide = await _repostory.GetByIdAsync(Id);
            return Ok(guide);

        }
        [HttpGet("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(int id, bool status)
        {
            await _repostory.ChangeStatusAsync(id, status);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGuide createGuide)
        {
            var guide = new Guide
            {
                Name=createGuide.Name,
                Description=createGuide.Description,
                Image=createGuide.Image,
                TiktokUrl=createGuide.TiktokUrl,
                InstagramUrl=createGuide.InstagramUrl,
                Status=createGuide.Status
            };
            var validationresult = _validator.Validate(guide);
            if (validationresult.IsValid)
            {
                await _repostory.InsertAsync(guide);
                return Ok("Melumat elave olundu");
            }
            else
            {
                return BadRequest(validationresult.Errors.Select(x => new
                {
                    PropertyName = x.PropertyName,
                    ErrorMessage = x.ErrorMessage
                }).ToList());
            }

        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateGuide updateGuide)
        {
            var guide = new Guide
            {
                Id=updateGuide.Id,
                Name = updateGuide.Name,
                Description = updateGuide.Description,
                Image = updateGuide.Image,
                TiktokUrl = updateGuide.TiktokUrl,
                InstagramUrl = updateGuide.InstagramUrl,
                Status = updateGuide.Status
            };


            var validationresult = _validator.Validate(guide);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(e => e.ErrorMessage));
            }
            await _repostory.UpdateAsync(guide);
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
