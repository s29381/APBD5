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
    public IActionResult Create([FromBody] DTO.DTO dto)
    {
        var success = _service.Create(dto);
        return success ? Created() : Conflict();
    }

    [HttpPut]
    public IActionResult Update([FromRoute] int id, [FromBody] DTO.DTO dto)
    {
        var success = _service.Update(id, dto);
        return success ? Ok() : Conflict();
    }

    [HttpDelete]
    public IActionResult Delete([FromRoute] int id)
    {
        var success = _service.Delete(id);
        return success ? NoContent() : Conflict();
    }
}