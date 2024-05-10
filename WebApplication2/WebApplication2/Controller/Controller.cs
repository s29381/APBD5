using Microsoft.AspNetCore.Mvc;
using WebApplication2.Service;

namespace WebApplication2.Controller;

[ApiController]
[Route("/api/animal")]
public class Controller : ControllerBase
{
    private IService _service;
    [HttpGet]
    public IActionResult Get([FromQuery] string? orderBy, IService service)
    {
        _service = service;
        var animals = service.GetAll(orderBy);
        return Ok(animals);
    }

    [HttpPost]
    public IActionResult Create([FromBody] DTO.DTO dto, IService service)
    {
        _service = service;
        var success = _service.Create(dto);
        return success ? Ok() : Conflict();
    }

    [HttpPut]
    public IActionResult Update(int id, [FromBody] DTO.DTO dto, IService service)
    {
        _service = service;
        var success = _service.Update(id, dto);
        return success ? Ok() : Conflict();
    }

    [HttpDelete]
    public IActionResult Delete(int id, IService service)
    {
        _service = service;
        var success = _service.Delete(id);
        return success ? Ok() : Conflict();
    }
}