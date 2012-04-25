using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace CodeBase.Helper
{
    public class MailHelper
    {

        public void sendMail(String to, String username)
        {
            var fromAddress = new MailAddress("from_mail", "CodeBase");
            var toAddress = new MailAddress(to, username);
            const string fromPassword = "";
            const string subject = "Subject";
            const string body = "Body";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}