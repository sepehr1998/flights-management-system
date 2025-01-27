namespace FlightsManagementBackend.Dtos;

public record BookDto(Guid FlightId, string PassengerEmail, byte NumberOfSeats);