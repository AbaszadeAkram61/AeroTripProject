using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.CQRS.Queries.Destinations.GetDestinationDropdown
{
    public class GetDestinationDropdownQueryResponse
    {
        public int Id { get; set; }
        public string City {  get; set; }
    }
}
