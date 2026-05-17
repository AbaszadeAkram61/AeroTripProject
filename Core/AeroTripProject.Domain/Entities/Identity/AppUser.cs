using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Domain.Entities.Identity
{
    public class AppUser:IdentityUser<int>
    {
        public string? ImageUrl {  get; set; }
        public string NameSurname {  get; set; }
        public ICollection<Reservation> Reservastions { get; set; }
        public ICollection<Comment> Comments { get; set; }
        
        
    }
}
