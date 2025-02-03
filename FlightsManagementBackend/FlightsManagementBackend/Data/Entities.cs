using FlightsManagementBackend.Domain.Entities;

namespace FlightsManagementBackend.Data;

public class Entities
{
    public IList<Passenger> Passengers = new List<Passenger>();
    public List<Flight> Flights = new List<Flight>();
}