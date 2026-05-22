using AeroTripProject.Application.Dtos.ContactUs;
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
    public class ContactUsesController : ControllerBase
    {
        private readonly IValidator<ContactUs> _validator;
        private readonly IRepostory<ContactUs> _repostory;

        public ContactUsesController(IValidator<ContactUs> validator, IRepostory<ContactUs> repostory)
        {
            _validator = validator;
            _repostory = repostory;
        }

        [HttpGet]
        public async Task<IActionResult> GetListContactUs()
        {
           return Ok( await _repostory.GetListAsync());

        }

        [HttpPost]
        public async Task<IActionResult> CreateContactus(CreateContactus createContactus)
        {
            ContactUs contactUs = new ContactUs
            {
                Name=createContactus.Name,
                Mail=createContactus.Mail,
                Subject=createContactus.Subject,
                MessageBody=createContactus.MessageBody,
                MessageDate=createContactus.MessageDate

            };

            var validationresult = _validator.Validate(contactUs);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(x => new
                {
                    PropertyName = x.PropertyName,
                    ErrorMessage = x.ErrorMessage
                }).ToList());
            }
            try
            {
                await _repostory.InsertAsync(contactUs);

                return Ok("Məlumat əlavə olundu");
            }
            catch (Exception)
            {
                return BadRequest("Məlumat əlavə olunarkən xəta baş verdi");
            }

        }

        [HttpGet("ChangeStatus/{id}/{status}")]
        public async Task<IActionResult> ChangeStatus(int id,bool status)
        {
            await _repostory.ChangeStatusAsync(id, status);
            return Ok("Status Deyisdirildi");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactUsById(int id)
        {
          return Ok( await _repostory.GetByIdAsync(id));
        }
    }
}
