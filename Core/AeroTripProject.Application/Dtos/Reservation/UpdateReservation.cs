using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.Dtos.Reservation
{
    public class UpdateReservation
    {
        public int Id { get; set; }
        public int DestinationId { get; set; }
        public string PersonCount { get; set; }

        public DateTime ReservationDate { get; set; }
        public string Description { get; set; }

        public string Status { get; set; }
    }
}
