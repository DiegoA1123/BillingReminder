using BillingReminder.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BillingReminder.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly IInvoiceRepository _repo;

    public InvoicesController(IInvoiceRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var data = await _repo.GetAll(ct);
        return Ok(data);
    }
}