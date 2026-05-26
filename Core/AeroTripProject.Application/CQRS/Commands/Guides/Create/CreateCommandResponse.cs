using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.CQRS.Commands.Guides.Create
{
    public class CreateCommandResponse
    {
        public string? PropertyName {  get; set; }
        public string? ErrorMessage { get; set; }
        public bool Success {  get; set; }
        public string? Message {  get; set; }
    }
}
