using FlightsManagementBackend.ReadModels;
using Microsoft.AspNetCore.Mvc;
using FlightsManagementBackend.Dtos;
using FlightsManagementBackend.Domain.Entities;
using FlightsManagementBackend.Domain.Errors;
using FlightsManagementBackend.Data;

namespace FlightsManagementBackend.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class FlightController : ControllerBase
{

    private readonly Entities _entities;

    public FlightController(Entities entities)
    {
        _entities = entities;
    }
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [ProducesResponseType(typeof(IEnumerable<FlightRm>), 200)]
    [HttpGet]
    public IEnumerable<FlightRm> Search()
    {
        var flightRmList = _entities.Flights.Select(flight => new FlightRm(
            flight.Id,
            flight.Airline,
            flight.Price,
            new TimePlaceRm(flight.Departure.Place.ToString(), flight.Departure.Time),
            new TimePlaceRm(flight.Arrival.Place.ToString(), flight.Arrival.Time),
            flight.RemainingNumberOfSeats
        ));
        return flightRmList;
    }

    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [ProducesResponseType(typeof(FlightRm), 200)]
    [HttpGet("{id}")]
    public ActionResult<FlightRm> Find(Guid id)
    {
        var flight = _entities.Flights.SingleOrDefault(f => f.Id == id);

        if (flight == null)
        {
            return NotFound();
        }

        var readModel = new FlightRm(
            flight.Id,
            flight.Airline,
            flight.Price,
            new TimePlaceRm(flight.Departure.Place.ToString(), flight.Departure.Time),
            new TimePlaceRm(flight.Arrival.Place.ToString(), flight.Arrival.Time),
            flight.RemainingNumberOfSeats
        );
        
        return Ok(readModel);
    }

    [HttpPost]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    [ProducesResponseType(200)]
    public IActionResult Book(BookDto dto)
    {
        var flight = _entities.Flights.SingleOrDefault(f => f.Id == dto.FlightId);
        if (flight == null)
            return NotFound();
        var error = flight.MakeBooking(dto.PassengerEmail, dto.NumberOfSeats);
        if (error is OverbookError)
            return Conflict(new { message = "Not enough seats" });
        return CreatedAtAction(nameof(Find), new { id = dto.FlightId });
    }

}