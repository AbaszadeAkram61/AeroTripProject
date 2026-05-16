using AeroTripProject.Application.Dtos.Destination;
using AeroTripProject.Application.Repostories;
using AeroTripProject.Domain.Entities;
using AeroTripProject.Persistence.Repostories;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AeroTripProject.WebApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationsController : ControllerBase
    {

        private readonly IRepostory<Destination> _repostory;
        private readonly IValidator<Destination> _validator;

        public DestinationsController(IRepostory<Destination> repostory, IValidator<Destination> validator)
        {
            _repostory = repostory;
            _validator = validator;
        }
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _repostory.GetListAsync());

        }
        [HttpGet("GetListName")]
        public async Task<IActionResult> GetListName()
        {
           return Ok( await _repostory.GetListNameAsync(x => x.City));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var destination = await _repostory.GetByIdAsync(Id);
            return Ok(destination);

        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDestination createDestination)
        {
            var destination = new Destination
            {
                City = createDestination.City,
                DayNight = createDestination.DayNight,
                Price = createDestination.Price,
                Image = createDestination.Image,
                CoverImage=createDestination.CoverImage,
                Description = createDestination.Description,
                Capacity = createDestination.Capacity,
                Details1=createDestination.Details1,
                Details2=createDestination.Details2,
                Image2=createDestination.Image2,
                Status = createDestination.Status
            };
            var validationresult = _validator.Validate(destination);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(x => x.ErrorMessage));

            }
            await _repostory.InsertAsync(destination);
            return Ok("Melumat elave olundu");
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateDestination updateDestination)
        {
            var destination = new Destination
            {
                Id = updateDestination.Id,
                City = updateDestination.City,
                DayNight = updateDestination.DayNight,
                Price = updateDestination.Price,
                Image = updateDestination.Image,
                CoverImage=updateDestination.CoverImage,
                Description = updateDestination.Description,
                Capacity = updateDestination.Capacity,
                Details1=updateDestination.Details1,
                Details2=updateDestination.Details2,
                Image2=updateDestination.Image2,
                Status = updateDestination.Status
            };


            var validationresult = _validator.Validate(destination);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(e => e.ErrorMessage));
            }
            await _repostory.UpdateAsync(destination);
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


        [HttpGet("GetDestinationDropdown")]
        public async Task<IActionResult> GetDestinationDropdown()
        {
            var values = await _repostory.GetSelectedListAsync(x => new ResultDestination
            {
                Id = x.Id,
                City = x.City
            });

            return Ok(values);
        }
    }
}
