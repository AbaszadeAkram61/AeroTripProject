using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.CQRS.Queries.Destinations.GetList
{
    public class GetListQueryRequest:IRequest<List<GetListQueryResponse>>
    {
    }
}
