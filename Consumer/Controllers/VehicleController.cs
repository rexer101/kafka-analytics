using Consumer.Infrastruture;
using Consumer.Services;
using Microsoft.AspNetCore.Mvc;

namespace Consumer.Controllers;

[ApiController]
[Route("[controller]")]
public class VehicleController : ControllerBase
{
    private readonly OverSpeedingService _service;

    public VehicleController(OverSpeedingService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAsync());
    }
}
