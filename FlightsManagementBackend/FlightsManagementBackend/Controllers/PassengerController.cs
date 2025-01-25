using Microsoft.AspNetCore.Mvc;
using FlightsManagementBackend.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FlightsManagementBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PassengerController
{
    static private IList<NewPassengerDto> Passengers = new List<NewPassengerDto>();
    
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public IActionResult Register(NewPassengerDto dto)
    {
        Passengers.Add(dto);
        return Ok();
    }
}