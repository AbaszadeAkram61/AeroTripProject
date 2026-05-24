using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.CQRS.Commands.Destinations.Delete
{
    public class DeleteCommandRequest:IRequest<DeleteCommandResponse>
    {
        public int Id { get; set; }
    }
}
