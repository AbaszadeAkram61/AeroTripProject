using AeroTripProject.Application.Dtos.Reservation;
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
    public class ReservationsController : ControllerBase
    {
        private readonly IRepostory<Reservation> _repostory;
        private readonly IValidator<Reservation> _validator;

        public ReservationsController(IRepostory<Reservation> repostory, IValidator<Reservation> validator)
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
            var Reservation = await _repostory.GetByIdAsync(Id);
            return Ok(Reservation);

        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateReservation createReservation)
        {
            var Reservation = new Reservation
            {
               AppUserId=createReservation.AppUserId,
               Destination=createReservation.Destination,
               PersonCount=createReservation.PersonCount,
               ReservationDate=createReservation.ReservationDate,
               Description=createReservation.Description,
               Status=createReservation.Status
               
            };
            var validationresult = _validator.Validate(Reservation);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(e => e.ErrorMessage));
            }
            await _repostory.InsertAsync(Reservation);
            return Ok("Melumat elave olundu");
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateReservation updateReservation)
        {
            var Reservation = new Reservation
            {
              Id=updateReservation.Id,
              Destination=updateReservation.Destination,
              PersonCount=updateReservation.PersonCount,
              ReservationDate=updateReservation.ReservationDate,
              Description=updateReservation.Description
            };


            var validationresult = _validator.Validate(Reservation);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(e => e.ErrorMessage));
            }
            await _repostory.UpdateAsync(Reservation);
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

        [HttpGet("GetListApprovalReservation/{appUserId}")]
        public async Task<IActionResult> GetListApprovalReservation(int appUserId)
        {
            var values = await _repostory.GetByIdListFilterAsyc(x =>
                x.AppUserId == appUserId &&
                x.Status == "Təsdiq Gözləyir");

            return Ok(values);
        }
    }
}
