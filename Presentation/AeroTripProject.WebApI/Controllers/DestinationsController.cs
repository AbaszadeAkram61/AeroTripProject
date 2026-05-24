using AeroTripProject.Application.CQRS.Commands.Destinations.Create;
using AeroTripProject.Application.CQRS.Commands.Destinations.Delete;
using AeroTripProject.Application.CQRS.Queries.Destinations.GetById;
using AeroTripProject.Application.CQRS.Queries.Destinations.GetCount;
using AeroTripProject.Application.CQRS.Queries.Destinations.GetDestinationDropdown;
using AeroTripProject.Application.CQRS.Queries.Destinations.GetList;
using AeroTripProject.Application.CQRS.Queries.Destinations.GetListName;
using AeroTripProject.Application.Dtos.Destination;
using AeroTripProject.Application.Repostories;
using AeroTripProject.Domain.Entities;
using AeroTripProject.Persistence.Repostories;
using FluentValidation;
using MediatR;
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
        private readonly IMediator _mediator;

        public DestinationsController(IRepostory<Destination> repostory, IValidator<Destination> validator, IMediator mediator)
        {
            _repostory = repostory;
            _validator = validator;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var response= await _mediator.Send(new GetListQueryRequest());
            return Ok(response);

        }
        [HttpGet("GetListName")]
        public async Task<IActionResult> GetListName()
        {
          var responses=await _mediator.Send(new GetListNameQueryRequest());
           return Ok(responses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediator.Send(
                new GetByIdQueryRequest
                {
                    Id = id
                });

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommandRequest createCommandRequest)
        {
            var response = await _mediator.Send(createCommandRequest);

            if (response.Any(x => !string.IsNullOrEmpty(x.Erorrmessage)))
            {
                return BadRequest(response);
            }

            return Ok(response);
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
           var response=await _mediator.Send(new DeleteCommandRequest
            {
                Id = Id
            });
            return Ok(response);
        }

        [HttpGet("Count")]
        public async Task<IActionResult> GetCount()
        {

            var response=await _mediator.Send(new GetCountQueryRequest());
            return Ok(response);
        }


        [HttpGet("GetDestinationDropdown")]
        public async Task<IActionResult> GetDestinationDropdown()
        {
           var response=await _mediator.Send(new GetDestinationDropdownQueryRequest());
            return Ok(response);
        }
    }
}
