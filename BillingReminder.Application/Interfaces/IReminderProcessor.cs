using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingReminder.Application.Interfaces
{
    public interface IReminderProcessor
    {
        Task Run(CancellationToken ct = default);
    }
}
