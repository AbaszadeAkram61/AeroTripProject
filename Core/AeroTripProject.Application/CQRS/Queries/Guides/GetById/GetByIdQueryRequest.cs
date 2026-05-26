using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.CQRS.Queries.Guides.GetById
{
    public class GetByIdQueryRequest:IRequest<GetByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
