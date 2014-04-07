using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace HelloJCE.Helpers
{
    public enum EmailTemplate
    {
        Registration,
        ResetPassword
    }

    public class Email
    {

        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="to">email to</param>
        /// <param name="username">to username</param>
        /// <param name="confirmationToken">token</param>
        /// <param name="emailForm">email form view</param>
        /// /// <param name="hostUrl">host url</param>
        public static void Send(string to, string username, string confirmationToken, EmailTemplate template)
        {
            var hostUrl = HttpContext.Current.Request.Url.Host;
            Thread thread = new Thread(() => SendEmailInBackground(to, username, confirmationToken, template, hostUrl));
            thread.Start();

            //dynamic email = new Email(emailForm);
            //email.To = to;
            //email.From = new System.Net.Mail.MailAddress("jce.teachme@gmail.com", "TeachMe Support");
            ////email.Subject = mail.Subject;
            //email.UserName = username;
            //email.ConfirmationToken = confirmationToken;
            //email.Send();
        }

        private static void SendEmailInBackground(string to, string username, string confirmationToken, EmailTemplate template, string hostUrl)
        {
            dynamic email = new Postal.Email(template.ToString());
            email.To = to;
            email.From = new System.Net.Mail.MailAddress("jce.teachme@gmail.com", "TeachMe Support");
            //email.Subject = mail.Subject;
            email.UserName = username;
            email.ConfirmationToken = confirmationToken;
            email.HostUrl = hostUrl;
            email.Send();
        }

       
    }
}