using AeroTripProject.Application.CQRS.Commands.Guides.Update;
using AeroTripProject.Application.CQRS.Commands.Guides.Create;
using AeroTripProject.Application.CQRS.Queries.Guides.ChangeStatus;
using AeroTripProject.Application.CQRS.Queries.Guides.GetById;
using AeroTripProject.Application.CQRS.Queries.Guides.GetList;
using AeroTripProject.Application.Dtos.Destination;
using AeroTripProject.Application.Dtos.Guide;
using AeroTripProject.Application.Repostories;
using AeroTripProject.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using AeroTripProject.Application.CQRS.Queries.Guides.GetCount;

namespace AeroTripProject.WebApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuidesController : ControllerBase
    {
        private readonly IRepostory<Guide> _repostory;
        private readonly IValidator<Guide> _validator;
        private readonly IMediator _mediator;

        public GuidesController(IRepostory<Guide> repostory, IValidator<Guide> validator, IMediator mediator)
        {
            _repostory = repostory;
            _validator = validator;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {

           var respnse=await _mediator.Send(new GetListQueryRequest());
            return Ok(respnse);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id )
        {
           var response=  await _mediator.Send(new GetByIdQueryRequest
            {
                Id = Id
            });

            return Ok(response);

        }
        [HttpGet("ChangeStatus/{Id}/{status}")]
        public async Task<IActionResult> ChangeStatus(int Id, bool status)
        {
           var response=await _mediator.Send(new ChangeStatusQueryRequest()
            {
                Id = Id,
                Status = status
            });
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommandRequest createCommandRequest)
        {
           var response=await _mediator.Send(createCommandRequest);
            if (response.Any(x=>x.Success==false))
            {
                return BadRequest(response);
            }
            return Ok(response);

        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCommandRequest updateCommandRequest)
        {
           var response=await _mediator.Send(updateCommandRequest);
            if (response.Any(x=>x.Success==false))
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    

        [HttpGet("Count")]
        public async Task<IActionResult> GetCount()
        {
            var response=await _mediator.Send(new GetCountQueryRequest());
            return Ok(response.Count);

        }
    }
}
