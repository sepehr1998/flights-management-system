using FlightsManagementBackend.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using FlightsManagementBackend.Dtos;
using FlightsManagementBackend.ReadModels;

namespace FlightsManagementBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PassengerController : ControllerBase
{
    static private IList<Passenger> Passengers = new List<Passenger>();
    
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public IActionResult Register(NewPassengerDto dto)
    {
        Passengers.Add(
            new Passenger(
                dto.Email,
                dto.FirstName,
                dto.LastName,
                dto.Gender
                )
            );
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