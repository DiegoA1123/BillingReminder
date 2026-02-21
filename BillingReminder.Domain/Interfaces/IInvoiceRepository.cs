using BillingReminder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingReminder.Domain.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<List<Invoice>> GetByStatus(string status, CancellationToken ct = default);
        Task<List<Invoice>> GetAll(CancellationToken ct = default);
        Task UpdateStatus(string invoiceId, string newStatus, CancellationToken ct = default);
    }
}
