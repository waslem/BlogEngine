using BlogEngine.Core.Infrastructure;
using System;
using System.Diagnostics;

namespace BlogEngine.Core.Services
{
    public class MockMailService : IMailService
    {

        public bool Send(string to, string from, string subject, string body)
        {
            String message = String.Format("To:{1}{1}From:{2}{0}Subject:{3}{0}Body:{4}{0}",
                Environment.NewLine,to, from, subject, body);

            Debug.WriteLine(message);
            return true;
        }

        public void SendRegisterEmail(string to, string username, string confirmationToken)
        {

        }
    }
}
