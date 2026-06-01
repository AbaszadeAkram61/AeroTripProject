using AeroTripProject.Application.Dtos.Contact;
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
    public class ContactsController : ControllerBase
    {
        private readonly IRepostory<Contact> _repostory;
        private readonly IValidator<Contact> _validator;

        public ContactsController(IRepostory<Contact> repostory, IValidator<Contact> validator)
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
            var Contact = await _repostory.GetByIdAsync(Id);
            return Ok(Contact);

        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateContact createContact)
        {
            var Contact = new Contact
            {
                Description = createContact.Description,
                Mail = createContact.Mail,
                Address = createContact.Address,
                Phone = createContact.Phone,
                MapLocation=createContact.MapLocation

            };
            var validationresult = _validator.Validate(Contact);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(x => new
                {
                    PropertyName=x.PropertyName,
                    ErrorMessage=x.ErrorMessage
                }));
            }
            await _repostory.InsertAsync(Contact);
            return Ok("Melumat elave olundu");
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateContact updateContact)
        {
            var Contact = new Contact
            {
                Id = updateContact.Id,
                Description = updateContact.Description,
                Mail = updateContact.Mail,
                Address = updateContact.Address,
                Phone = updateContact.Phone,
                MapLocation=updateContact.MapLocation

            };


            var validationresult = _validator.Validate(Contact);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(x => new
                {
                    PropertyName = x.PropertyName,
                    ErrorMessage = x.ErrorMessage
                }));
            }
           
            await _repostory.UpdateAsync(Contact);
            return Ok("Melumat deyisdirildi");
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            await _repostory.DeleteAsync(Id);
            return Ok("Melumat silindi");
        }

       
    }
}
