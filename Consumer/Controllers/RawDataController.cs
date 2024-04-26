using Consumer.Infrastruture;
using Consumer.Services;
using Microsoft.AspNetCore.Mvc;

namespace Consumer.Controllers;

[ApiController]
[Route("[controller]")]
public class RawDataController : ControllerBase
{
    private readonly RawDataService _service;

    public RawDataController(RawDataService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAsync());
    }
}
