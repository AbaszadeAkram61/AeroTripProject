using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.CQRS.Queries.Guides.GetList
{
    public class GetListQueryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string TiktokUrl { get; set; }
        public string InstagramUrl { get; set; }
        public bool Status {  get; set; }
        
    }
}
