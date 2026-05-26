using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.CQRS.Queries.Guides.ChangeStatus
{
    public class ChangeStatusQueryRequest:IRequest<ChangeStatusQueryResponse>
    {
        public int Id {  get; set; }
        public bool Status {  get; set; }
    }
}
