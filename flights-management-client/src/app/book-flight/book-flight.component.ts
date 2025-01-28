import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { FlightService } from "../api/services/flight.service";
import { FlightRm } from "../api/models/flight-rm";
import { DatePipe } from "@angular/common";
import { AuthService } from "../auth/auth.service";
import { FormBuilder, FormsModule, ReactiveFormsModule } from "@angular/forms";
import {BookDto} from "../api/models/book-dto";

@Component({
  selector: 'app-book-flight',
  imports: [
    DatePipe,
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './book-flight.component.html',
  styleUrl: './book-flight.component.css',
  standalone: true,

})
export class BookFlightComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private flightService: FlightService,
    private router: Router,
    private auth: AuthService,
    private fb: FormBuilder) { }
  flightId: string = 'not loaded'
  flight: FlightRm = {}

  form = this.fb.group({
    number: [1],
  })

  ngOnInit() {
    if (!this.auth.currentUser)
      this.router.navigate(['/register-passenger'])
    this.route.paramMap.subscribe(p=>this.findFlight(p.get("flightId")))
  }

  private findFlight = (flightId: string | null) => {
    this.flightId = flightId ?? 'not passed'
    this.flightService.findFlight({id: this.flightId})
      .subscribe(flight => this.flight = flight, this.handleError)
  }
  private handleError = (error:any)=> {
    if (error.status == 404){
      alert("Flight not found!")
      this.router.navigate(['/search-flights'])
    }
    console.log("Response Error Message", error.statusText)
    console.log(error)
  }

  book() {
    const booking: BookDto = {
      flightId: this.flightId,
      passengerEmail: this.auth.currentUser?.email,
      numberOfSeats: this.form.get('number')?.value
    }

    this.flightService.bookFlight({ body: booking })
      .subscribe(_=>console.log("succeeded"), console.error)
  }
}
