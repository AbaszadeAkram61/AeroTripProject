using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.Dtos.User
{
    public class CreateUser
    {
        [Required(ErrorMessage ="Zəhmət olmasa adınızı və soyadınızı yazın")]
        public string NameSurname {  get; set; }

        [Required(ErrorMessage = "Zəhmət olmasa emailinizi yazın")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Zəhmət olmasa istifadəçi adınızı yazın")]
        public string Username {  get; set; }

        [Required(ErrorMessage = "Zəhmət olmasa şifrənizi  yazın")]
        public string Password {  get; set; }

        [Required(ErrorMessage = "Şifrələr uyğun deyil")]
        public string PasswordConfirm {  get; set; }
    }
}
