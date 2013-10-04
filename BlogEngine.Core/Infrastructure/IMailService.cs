using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Infrastructure
{
    public interface IMailService
    {
        bool Send(string to, string from, string subject, string body);

        void SendRegisterEmail(string to, string username, string confirmationToken);
    }
}
