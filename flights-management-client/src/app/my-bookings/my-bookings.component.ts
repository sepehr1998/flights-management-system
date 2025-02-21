import { Component, OnInit } from '@angular/core';
import { BookingRm, BookDto } from "../api/models";
import { BookingService } from "../api/services/booking.service";
import { AuthService } from "../auth/auth.service";
import {DatePipe} from "@angular/common";

@Component({
  selector: 'app-my-bookings',
  imports: [
    DatePipe
  ],
  templateUrl: './my-bookings.component.html',
  standalone: true,
  styleUrl: './my-bookings.component.css'
})
export class MyBookingsComponent implements OnInit{
  bookings!: BookingRm[];
  constructor(
    private bookingService: BookingService,
    private authService: AuthService) {}
  ngOnInit(): void {
    this.bookingService.listBooking({ email: this.authService.currentUser?.email ?? '' })
      .subscribe(r=>this.bookings = r, this.handleError);
  }
  private handleError(error: any){
    console.log("Response Error, Status: " + error.status);
  }

  cancel(booking: BookingRm) {
    const dto: BookDto = {
      flightId: booking.flightId,
      numberOfSeats: booking.numberOfBookedSeats,
      passengerEmail: booking.passengerEmail
    }
    this.bookingService.cancelBooking({body: dto})
      .subscribe(_ => {this.bookings = this.bookings.filter(b=>b != booking)},
        this.handleError)
  }
}
