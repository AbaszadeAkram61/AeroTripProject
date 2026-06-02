using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.Dtos.Mail
{
    public class MailRequest
    {
        public string? Name {  get; set; }
      
        public string? ReceiverMail {  get; set; }
        public string? Subject {  get; set; }
        public string? Body { get; set; }
    }
}
