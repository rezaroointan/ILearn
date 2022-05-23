using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IEmailSender
    {
        public Task SendAsync(string toEmail, string subject, string body, bool isBodyHtml = false);
    }
}
