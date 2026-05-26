using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.CQRS.Commands.Destinations.Create
{
    public class CreateCommandResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public string? Propertyname { get; set; }
        public string? Errormessage { get; set; }
    }
}
