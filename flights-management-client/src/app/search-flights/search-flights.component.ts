import { Component } from '@angular/core';
import {DatePipe, NgForOf} from "@angular/common";
import { FlightService } from "../api/services/flight.service";
import { FlightRm } from "../api/models/flight-rm";

@Component({
  selector: 'app-search-flights',
  standalone: true,
  imports: [
    NgForOf,
    DatePipe
  ],
  templateUrl: './search-flights.component.html',
  styleUrl: './search-flights.component.css'
})
export class SearchFlightsComponent {
  constructor(private flightService: FlightService) {}
  ngOnInit(): void {

  }
  searchResult: FlightRm[] = []
  search(){
    this.flightService.searchFlight({}).subscribe(response=>this.searchResult = response,
      this.handleError)
  }
  private handleError(error:any){
    console.log(error)
  }
}
