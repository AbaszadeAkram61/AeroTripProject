using AeroTripProject.Application.Dtos.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Net.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using FluentValidation;


namespace AeroTripProject.WebApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailsController : ControllerBase
    {
        private readonly IValidator<MailRequest> _validator;

        public MailsController(IValidator<MailRequest> validator)
        {
            _validator = validator;
        }

        [HttpPost]
        public IActionResult SendMail(MailRequest mailRequest)

        {

            var validationResult = _validator.Validate(mailRequest);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(x => new
                {
                    PropertyName = x.PropertyName,
                    ErrorMessage = x.ErrorMessage
                }).ToList());
            }
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom =
                new MailboxAddress(mailRequest.Name, "abaszadeakram61@gmail.com");

            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo =
                new MailboxAddress("User", mailRequest.ReceiverMail);

            mimeMessage.To.Add(mailboxAddressTo);

            mimeMessage.Subject = mailRequest.Subject;

            mimeMessage.Body = new TextPart("plain")
            {
                Text = mailRequest.Body
            };

            MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient();

            client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
            client.Authenticate("abaszadeakram61@gmail.com", "xqrtagngrlgzehjl");
            client.Send(mimeMessage);
            client.Disconnect(true);

            return Ok("Mail uğurla göndərildi");
        }
    }
}
