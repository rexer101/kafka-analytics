using System.Text.Json;
using Consumer.Dto;
using Consumer.Infrastruture;
using Consumer.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
    [HttpPost]
    public IActionResult Post(string data)
    {
        try
        {
            var vehicledata = JsonConvert.DeserializeObject<Messages>(data);
            return Ok(vehicledata);
        }
        catch(Exception ex)
        {
            throw;
        }
    }
}
