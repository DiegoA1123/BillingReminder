using BillingReminder.Application.Services;
using BillingReminder.Domain.Constants;
using BillingReminder.Domain.Entities;
using BillingReminder.Domain.Interfaces;
using Moq;
using Xunit;

public class ReminderProcessorTests
{
    [Fact]
    public async Task SendFirstAndSecondRecordatory()
    {
        var repo = new Mock<IInvoiceRepository>();
        var email = new Mock<IEmailSender>();

        repo.Setup(r => r.GetByStatus(InvoiceStatus.PrimerRecordatorio, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Invoice> {
            new Invoice { Id="1", ClientEmail="a@a.com", Status=InvoiceStatus.PrimerRecordatorio }
            });

        repo.Setup(r => r.GetByStatus(InvoiceStatus.SegundoRecordatorio, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Invoice>());

        var processor = new ReminderProcessor(repo.Object, email.Object);

        await processor.Run();

        email.Verify(e => e.Send("a@a.com", It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        repo.Verify(r => r.UpdateStatus("1", InvoiceStatus.SegundoRecordatorio, It.IsAny<CancellationToken>()), Times.Once);
    }
}