using AeroTripProject.Application.Dtos.Reservation;
using AeroTripProject.Application.Repostories;
using AeroTripProject.Domain.Entities;
using AeroTripProject.Domain.Entities.Identity;
using AeroTripProject.Persistence.Repostories;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
               DestinationId=createReservation.DestinationId,
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
              DestinationId=updateReservation.DestinationId,
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

        [HttpGet("GetListCurrentReservation/{appUserId}")]
        public async Task<IActionResult> GetListCurrentReservation(int appUserId)
        {
            var values = await _repostory.GetByIdListFilterAsyc(x =>
               x.AppUserId == appUserId &&
               x.Status == "Aktiv");

            return Ok(values);
        }

        [HttpGet("GetListOldReservation/{appUserId}")]
        public async Task<IActionResult> GetListOldReservation(int appUserId)
        {
            var values = await _repostory.GetByIdListFilterAsyc(x =>
             x.AppUserId == appUserId &&
             x.Status == "Keçmiş");

            return Ok(values);
        }

        [HttpGet("GetListCurrentReservation")]
        public async Task<IActionResult> GetListCurrentReservation()
        {
            var values =await _repostory.GetByIdListFilterAsyc(x => x.Status == "Aktiv");
            return Ok(values.Count);
        }

        [HttpGet("TotalRevenue")]
        public async Task<IActionResult> TotalRevenue()
        {
          return Ok( await _repostory.GetListFilterSumAsyc(
             x => x.Status == "Aktiv",
             x => (int)x.Destination.Price
                                           ));
        }

        [HttpGet("ApprovalRevenue")]
        public async Task<IActionResult> ApprovalRevenue()
        {
            return Ok(await _repostory.GetListFilterSumAsyc(
            x => x.Status == "Təsdiq Gözləyir",
            x => (int)x.Destination.Price
                                          ));
        }
    }
}
