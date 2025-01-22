import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { FlightService } from "../api/services/flight.service";
import { FlightRm } from "../api/models/flight-rm";
import { DatePipe } from "@angular/common";

@Component({
  selector: 'app-book-flight',
  imports: [
    DatePipe
  ],
  templateUrl: './book-flight.component.html',
  styleUrl: './book-flight.component.css',
  standalone: true,

})
export class BookFlightComponent implements OnInit {
  constructor(private route: ActivatedRoute, private flightService: FlightService) { }
  flightId: string = 'not loaded'
  flight: FlightRm = {}
  ngOnInit() {
    this.route.paramMap.subscribe(p=>this.findFlight(p.get("flightId")))
  }

  private findFlight = (flightId: string | null) => {
    this.flightId = flightId ?? 'not passed'
    this.flightService.findFlight({id: this.flightId})
      .subscribe(flight => this.flight = flight)
  }
}
