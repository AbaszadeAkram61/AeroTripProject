using AeroTripProject.Application.CQRS.Commands.Destinations.Create;
using AeroTripProject.Application.CQRS.Commands.Destinations.Delete;
using AeroTripProject.Application.CQRS.Commands.Destinations.Update;
using AeroTripProject.Application.CQRS.Queries.Destinations.GetById;
using AeroTripProject.Application.CQRS.Queries.Destinations.GetCount;
using AeroTripProject.Application.CQRS.Queries.Destinations.GetDestinationDropdown;
using AeroTripProject.Application.CQRS.Queries.Destinations.GetList;
using AeroTripProject.Application.CQRS.Queries.Destinations.GetListName;
using AeroTripProject.Application.Dtos.Destination;
using AeroTripProject.Application.Repostories;
using AeroTripProject.Domain.Entities;
using AeroTripProject.Persistence.Repostories;
using Azure.Core;
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

   
        private readonly IMediator _mediator;

        public DestinationsController(IMediator mediator)
        {
          
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

            if (response.Any(x => x.IsSuccess == false))
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCommandRequest updateCommandRequest)
        {
          var response= await _mediator.Send(updateCommandRequest);
            if (response.Any(x=>x.Success==false))
            {
                return BadRequest(response);
            }
            return Ok(response);
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
            return Ok(response.Count);
        }


        [HttpGet("GetDestinationDropdown")]
        public async Task<IActionResult> GetDestinationDropdown()
        {
           var response=await _mediator.Send(new GetDestinationDropdownQueryRequest());
            return Ok(response);
        }
    }
}
