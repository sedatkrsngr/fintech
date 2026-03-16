using Microsoft.AspNetCore.Mvc;

namespace Fintech.HistoryQueryService.Controllers;

[ApiController]
[Route("health")]
public sealed class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            status = "ok",
            service = "history-query-service"
        });
    }
}
