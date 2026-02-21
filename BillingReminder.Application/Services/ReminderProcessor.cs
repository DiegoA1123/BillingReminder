using BillingReminder.Application.Interfaces;
using BillingReminder.Domain.Constants;
using BillingReminder.Domain.Interfaces;
using BillingReminder.Infrastructure.Email;

namespace BillingReminder.Application.Services;

public class ReminderProcessor : IReminderProcessor
{
    private readonly IInvoiceRepository _invoiceRepo;
    private readonly IEmailSender _emailSender;

    public ReminderProcessor(IInvoiceRepository invoiceRepo, IEmailSender emailSender)
    {
        _invoiceRepo = invoiceRepo;
        _emailSender = emailSender;
    }

    public async Task Run(CancellationToken ct = default)
    {
        await ProcessFirstReminder(ct);
        await ProcessSecondReminder(ct);
    }

    private async Task ProcessFirstReminder(CancellationToken ct)
    {
        var invoices = await _invoiceRepo.GetByStatus(InvoiceStatus.PrimerRecordatorio, ct);

        foreach (var inv in invoices) {

            var subject = "Actualización de tu cuenta";
            var body = await EmailTemplateRenderer.Render(
                "first-reminder.html",
                new Dictionary<string, string>
                {
                    ["{{CLIENT_ID}}"] = inv.ClientId,
                    ["{{INVOICE_ID}}"] = inv.Id,
                    ["{{TOTAL}}"] = inv.Total.ToString("N0"),
                    ["{{STATUS}}"] = inv.Status
                },
                ct
            );

            await _emailSender.Send(inv.ClientEmail, subject, body, ct);
            await _invoiceRepo.UpdateStatus(inv.Id, InvoiceStatus.SegundoRecordatorio, ct);

        }
    }

    private async Task ProcessSecondReminder(CancellationToken ct)
    {
        var invoices = await _invoiceRepo.GetByStatus(InvoiceStatus.SegundoRecordatorio, ct);

        foreach (var inv in invoices) {

            var subject = "Aviso importante";
            var body = await EmailTemplateRenderer.Render(
                "second-reminder.html",
                new Dictionary<string, string>
                {
                    ["{{CLIENT_ID}}"] = inv.ClientId,
                    ["{{INVOICE_ID}}"] = inv.Id,
                    ["{{TOTAL}}"] = inv.Total.ToString("N0"),
                    ["{{STATUS}}"] = inv.Status
                },
                ct
            );

            await _emailSender.Send(inv.ClientEmail, subject, body, ct);
            await _invoiceRepo.UpdateStatus(inv.Id, InvoiceStatus.Desactivado, ct);

        }

    }

}
