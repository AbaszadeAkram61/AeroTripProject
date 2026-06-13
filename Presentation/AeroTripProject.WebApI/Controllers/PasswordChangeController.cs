using AeroTripProject.Application.Dtos.Mail;
using AeroTripProject.Application.Dtos.Password;
using AeroTripProject.Domain.Entities.Identity;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace AeroTripProject.WebApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordChangeController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public PasswordChangeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPassword forgetPassword)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(forgetPassword.Mail);

                if (user == null)
                {
                    return BadRequest("İstifadəçi tapılmadı");
                }

                string passwordResetToken =
                    await _userManager.GeneratePasswordResetTokenAsync(user);

                var passwordResetTokenLink =
                 $"https://aerotrip-001-site1.rtempurl.com/Admin/PasswordChanges/ResetPassword?userId={user.Id}&token={Uri.EscapeDataString(passwordResetToken)}";

                MimeMessage mimeMessage = new MimeMessage();

                mimeMessage.From.Add(
                    new MailboxAddress("AeroTrip Admin", "abaszadeakram61@gmail.com"));

                mimeMessage.To.Add(
                    new MailboxAddress("AeroTrip User", forgetPassword.Mail));

                mimeMessage.Subject = "AeroTrip | Parolun Yenilənməsi";

                mimeMessage.Body = new TextPart("plain")
                {
                    Text = $"Parolunuzu yeniləmək üçün linkə daxil olun:\n\n{passwordResetTokenLink}"
                };

                using var client = new MailKit.Net.Smtp.SmtpClient();

                client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);

                client.Authenticate(
                    "abaszadeakram61@gmail.com",
                    "xqrtagngrlgzehjl");

                client.Send(mimeMessage);

                client.Disconnect(true);

                return Ok("Mail göndərildi");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest("Parollar uyğun gəlmir.");
            }

            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                return BadRequest("İstifadəçi tapılmadı.");
            }

            var result = await _userManager.ResetPasswordAsync(
                user,
                model.Token,
                model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(x => x.Description));
            }

            return Ok("Parol uğurla yeniləndi.");
        }

    }
}

