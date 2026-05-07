using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.Dtos.User
{
    using System.ComponentModel.DataAnnotations;

    public class UserSignUp
    {
        [Required(ErrorMessage = "Zəhmət olmasa adınızı və soyadınızı yazın")]
        public string NameSurname { get; set; }

        [Required(ErrorMessage = "Zəhmət olmasa emailinizi yazın")]
        [EmailAddress(ErrorMessage = "Email formatı düzgün deyil")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Zəhmət olmasa istifadəçi adınızı yazın")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Zəhmət olmasa şifrənizi yazın")]

        [MinLength(6, ErrorMessage = "Şifrə minimum 6 simvol olmalıdır")]

     
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Şifrələr uyğun deyil")]
        public string PasswordConfirm { get; set; }
    }
}
