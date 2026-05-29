using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.Dtos.Reservation
{
    public class ResultReservation
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public int DestinationId { get; set; }
        public int PersonCount { get; set; }
        public string DestinationName { get; set; }

        public DateTime ReservationDate { get; set; }
        public string Description { get; set; }
        public string StatusString { get; set; }
    }
}
