using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.Dtos.User
{
    public class UserSignIn
    {

        [Required(ErrorMessage = "Zəhmət olmasa istifadəçi adınızı yazın")]
        public string Username {  get; set; }

        [Required(ErrorMessage = "Zəhmət olmasa şifrənizi yazın")]

        [MinLength(6, ErrorMessage = "Şifrə minimum 6 simvol olmalıdır")]
        public string Password { get; set; }
    }
}
