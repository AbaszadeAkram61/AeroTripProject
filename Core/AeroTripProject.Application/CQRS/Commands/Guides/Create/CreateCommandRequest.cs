using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.CQRS.Commands.Guides.Create
{
    public class CreateCommandRequest:IRequest<List<CreateCommandResponse>>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? TiktokUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public bool Status { get; set; }
    }
}
