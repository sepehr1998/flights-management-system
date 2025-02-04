using Microsoft.AspNetCore.Mvc;
using FlightsManagementBackend.Data;
using FlightsManagementBackend.ReadModels;

namespace FlightsManagementBackend.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly Entities _entities;

    public BookingController(Entities entities)
    {
        _entities = entities;
    }

    [HttpGet("{email}")]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    [ProducesResponseType(200)]
    public ActionResult<IEnumerable<BookingRm>> List(string email)
    {
        var bookings = _entities.Flights.ToArray()
            .SelectMany(f => f.Bookings
                .Where(b => b.PassengerEmail == email)
                .Select(b => new BookingRm(
                    f.Id,
                    f.Airline,
                    f.Price.ToString(),
                    new TimePlaceRm(f.Arrival.Place, f.Arrival.Time),
                    new TimePlaceRm(f.Departure.Place, f.Departure.Time),
                    b.NumberOfSeats,
                    email
                ))
            );
        return Ok(bookings);
    }
}