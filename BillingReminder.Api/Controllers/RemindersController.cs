using BillingReminder.Application.Interfaces;
using BillingReminder.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BillingReminder.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RemindersController : ControllerBase
{
    private readonly IReminderProcessor _processor;

    public RemindersController(IReminderProcessor processor)
    {
        _processor = processor;
    }

    [HttpPost("run")]
    public async Task<IActionResult> Run(CancellationToken ct)
    {
        await _processor.Run(ct);
        return Ok(new { message = "Recordatorios enviados" });
    }
}