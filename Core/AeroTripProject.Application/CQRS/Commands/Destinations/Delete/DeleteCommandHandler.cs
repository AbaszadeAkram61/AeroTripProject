using AeroTripProject.Application.Repostories;
using AeroTripProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.CQRS.Commands.Destinations.Delete
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommandRequest, DeleteCommandResponse>
    {
        private readonly IRepostory<Destination> _repostory;

        public DeleteCommandHandler(IRepostory<Destination> repostory)
        {
            _repostory = repostory;
        }

        public async Task<DeleteCommandResponse> Handle(DeleteCommandRequest request, CancellationToken cancellationToken)
        {
            await _repostory.DeleteAsync(request.Id);
            return new DeleteCommandResponse
            {
                Message = "Melumat silindi"
            };
            
        }
    }
}
