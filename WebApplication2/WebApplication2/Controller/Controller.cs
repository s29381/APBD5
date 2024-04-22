using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controller;

[ApiController]
[Route("/api/animal")]
public class Controller : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get([FromQuery] string orderBy)
    {
        var animals = Service.Service.GetAll(orderBy);
        return Ok(animals);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult Create([FromBody] DTO.DTO dto)
    {
        var success = Service.Service.Create(dto);
        return success ? Created() : Conflict();
    }

    [HttpPut("/api/animal/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult Update([FromRoute] int id, [FromBody] DTO.DTO dto)
    {
        var success = Service.Service.Update(id, dto);
        return success ? Ok() : Conflict();
    }

    [HttpDelete("/api/animal/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult Delete([FromRoute] int id)
    {
        var success = Service.Service.Delete(id);
        return success ? NoContent() : Conflict();
    }
}