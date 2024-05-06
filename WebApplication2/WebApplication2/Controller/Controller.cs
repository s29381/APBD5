using Microsoft.AspNetCore.Mvc;
using WebApplication2.Service;

namespace WebApplication2.Controller;

[ApiController]
[Route("/api/animal")]
public class Controller(IService service) : ControllerBase
{
    [HttpGet]
    public IActionResult Get([FromQuery] string? orderBy)
    {
        var animals = service.GetAll(orderBy);
        return Ok(animals);
    }

    [HttpPost]
    public IActionResult Create([FromBody] DTO.DTO dto)
    {
        var success = service.Create(dto);
        return success ? Created() : Conflict();
    }

    [HttpPut]
    public IActionResult Update([FromRoute] int id, [FromBody] DTO.DTO dto)
    {
        var success = service.Update(id, dto);
        return success ? Ok() : Conflict();
    }

    [HttpDelete]
    public IActionResult Delete([FromRoute] int id)
    {
        var success = service.Delete(id);
        return success ? NoContent() : Conflict();
    }
}