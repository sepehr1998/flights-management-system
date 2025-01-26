using Microsoft.AspNetCore.Mvc;
using FlightsManagementBackend.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using FlightsManagementBackend.ReadModels;

namespace FlightsManagementBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PassengerController : ControllerBase
{
    static private IList<NewPassengerDto> Passengers = new List<NewPassengerDto>();
    
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public IActionResult Register(NewPassengerDto dto)
    {
        Passengers.Add(dto);
        return CreatedAtAction(nameof(Find), new { email = dto.Email });
    }

    [HttpGet("{email}")]
    public ActionResult<PassengerRm> Find(string email)
    {
        var passenger = Passengers.FirstOrDefault(p => p.Email == email);

        if (passenger == null)
            return NotFound();

        var rm = new PassengerRm(
            passenger.Email,
            passenger.FirstName,
            passenger.LastName,
            passenger.Gender
        );

        return Ok(rm);
    }
}