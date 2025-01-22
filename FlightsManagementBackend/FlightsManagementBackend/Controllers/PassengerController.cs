using Microsoft.AspNetCore.Mvc;
using FlightsManagementBackend.Dtos;

namespace FlightsManagementBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PassengerController
{
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public IActionResult Register(NewPassengerDto dto)
    {
        throw new NotImplementedException();
    }
}