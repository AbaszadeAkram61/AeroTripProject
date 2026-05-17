using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.Dtos.User
{
    public  class ResultUsers
    {
        public int Id { get; set; }
        public string ImageUrl {  get; set; }
        public string NameSurname {  get; set; }
        public string Username {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm {  get; set; }
    }
}
