import { Component } from '@angular/core';
import {DatePipe, NgForOf} from "@angular/common";

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
  searchResult: FlightRm[] = [
    {
      airline: "American Airlines",
      remainingNumberOfSeats: 500,
      departure: { time: Date.now().toString(), place: "Los Angeles" },
      arrival: { time: Date.now().toString(), place: "Istanbul" },
      price: "350",
    },
    {
      airline: "Deutsche BA",
      remainingNumberOfSeats: 60,
      departure: { time: Date.now().toString(), place: "Munchen" },
      arrival: { time: Date.now().toString(), place: "Schiphol" },
      price: "600",
    },
    {
      airline: "British Airways",
      remainingNumberOfSeats: 60,
      departure: { time: Date.now().toString(), place: "London, England" },
      arrival: { time: Date.now().toString(), place: "Vizzola-Ticino" },
      price: "600",
    },
  ]
}

export interface FlightRm {
  airline: string
  arrival: TimePlaceRm
  departure: TimePlaceRm
  price: string
  remainingNumberOfSeats: number
}

export interface TimePlaceRm {
  place: string
  time: string
}
