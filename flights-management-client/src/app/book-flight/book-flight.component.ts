import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: 'app-book-flight',
  imports: [],
  templateUrl: './book-flight.component.html',
  styleUrl: './book-flight.component.css',
  standalone: true,

})
export class BookFlightComponent implements OnInit {
  constructor(private route: ActivatedRoute) { }
  flightId: string = 'not loaded'
  ngOnInit() {
    this.route.paramMap.subscribe(p=>this.findFlight(p.get("flightId")))
  }

  private findFlight = (flightId: string | null) => {
    this.flightId = flightId ?? 'not passed'
  }
}
