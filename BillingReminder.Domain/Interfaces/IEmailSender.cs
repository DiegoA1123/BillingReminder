using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingReminder.Domain.Interfaces
{
    public interface IEmailSender
    {
        Task Send(string toEmail, string subject, string body, CancellationToken ct = default);
    }
}
