using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Domain.Entities
{
    public class About1:BaseEntity
    {
       
        public string Title1 {  get; set; }

        public string Description1 {  get; set; }
        public string Image1 {  get; set; }
        public string Title2 { get; set; }

        public string Description2 { get; set; }
        public bool Status {  get; set; }
    }
}
