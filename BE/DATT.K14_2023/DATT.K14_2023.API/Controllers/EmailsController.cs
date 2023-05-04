using EmailService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace DATT.K14_2023.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        public EmailsController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost]
        public bool SendMail(string to, string subject, string body)
        {
            try
            {
                var toCustomer = to.Split(',');
                foreach(var customer in toCustomer) 
                {
                    var message = new Message(new string[] { customer }, subject, body);
                    _emailSender.SendEmail(message);
                }
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }
    }
}
