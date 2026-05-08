namespace AeroTripProject.WebUI.Areas.Member.Models
{
    public class EditUserViewModel
    {
        public string NameSurname {  get; set; }
        public string Password {  get; set; }
        public string PasswordConfirm {  get; set; }
        public string Username {  get; set; }
        public string PhoneNumber {  get; set; }
        public string Email { get; set; }
        public string ImageUrl {  get; set; }
    }
}
