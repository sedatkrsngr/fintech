using Microsoft.AspNetCore.Mvc;

namespace Fintech.ApiGateway.Controllers;

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
            service = "api-gateway"
        });
    }
}
