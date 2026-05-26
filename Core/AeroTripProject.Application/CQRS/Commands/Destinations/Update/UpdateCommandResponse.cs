using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.CQRS.Commands.Destinations.Update
{
    public class UpdateCommandResponse
    {
        public string? Propertyname {  get; set; }
        public string? Errormessage { get; set; }
        public bool Success { get; set; }
        public string? Message {  get; set; }
    }
}
