using AeroTripProject.Application.Dtos.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace AeroTripProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MailController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMail(MailRequest mailRequest)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress(mailRequest.Name, "abaszadeakram61@gmail.com");

            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", mailRequest.ReceiverMail);

            mimeMessage.To.Add(mailboxAddressTo);

            mimeMessage.Subject = mailRequest.Subject;

            mimeMessage.Body = new TextPart("plain")
            {
                Text = mailRequest.Body
            };


            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
            client.Authenticate(
      "abaszadeakram61@gmail.com",
      "xqrtagngrlgzehjl"
  );
            client.Send(mimeMessage);
            client.Disconnect(true);
           

            TempData["MailSuccess"] = "Mail uğurla göndərildi";

            return RedirectToAction("Index");

        }
    }
}
