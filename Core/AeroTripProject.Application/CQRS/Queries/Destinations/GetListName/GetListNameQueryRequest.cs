using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.CQRS.Queries.Destinations.GetListName
{
    public class GetListNameQueryRequest:IRequest<List<GetListNameQueryResponse>>
    {
    }
}
