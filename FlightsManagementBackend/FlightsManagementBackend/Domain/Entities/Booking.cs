namespace FlightsManagementBackend.Domain.Entities;

public record Booking(
    string PassengerEmail, 
    byte NumberOfSeats
);