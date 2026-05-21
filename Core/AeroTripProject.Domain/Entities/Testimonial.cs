using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Domain.Entities
{
    public class Testimonial:BaseEntity
    {
        
        public string Client {  get; set; }
        public string Comment {  get; set; }
        public string ClientImage {  get; set; }
       
    }
}
