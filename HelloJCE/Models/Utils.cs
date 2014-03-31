using Postal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloJCE.Models
{
    public class Utils
    {
        public static string Check(double lower, double upper, double toCheck)
        {
            string str = toCheck > lower && toCheck <= upper ? " checked=\"checked\"" : null;
            return toCheck > lower && toCheck <= upper ? " checked=\"checked\"" : null;
        }

        /// <summary>
        /// Send email usin Postal package
        /// </summary>
        /// <param name="viewName">name of View to use for email body</param>
        /// <param name="to">to adresses</param>
        /// <param name="username">from adress</param>
        /// <param name="confirmationToken"></param>
        public static void SendEmail(string viewName, string to, string username, string confirmationToken)
        {
            viewName = "RegEmail";
            dynamic email = new Email(viewName);
            email.To = to;
            email.UserName = username;
            email.ConfirmationToken = confirmationToken;
            email.Send();
        }
    }
}