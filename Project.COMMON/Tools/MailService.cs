using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Project.COMMON.Tools
{
    public static class MailService
    {
        public static void Send(string receiver,string passWord = "atlkbjteiruyhcww", string sender = "yzl3157test@gmail.com",string subject ="Email Test",string body="Hey")
        {
            MailAddress senderEmail = new MailAddress(sender);
            MailAddress receiverEmail = new MailAddress(receiver);

            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587, // Kurumsal olursa 465 olucak
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, passWord)
            };
            using (MailMessage message = new MailMessage(senderEmail,receiverEmail)
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
