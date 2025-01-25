import { Component } from '@angular/core';
import { PassengerService } from "../api/services/passenger.service";
import {FormsModule, ReactiveFormsModule, FormBuilder, Validators} from "@angular/forms";

@Component({
  selector: 'app-register-passenger',
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './register-passenger.component.html',
  standalone: true,
  styleUrl: './register-passenger.component.css'
})
export class RegisterPassengerComponent {
  constructor(private passengerService: PassengerService, private fb: FormBuilder) {}

  form = this.fb.group({
    email: [''],
    firstName: [''],
    lastName: [''],
    isFemale: [true]
  })

  register() {
    this.passengerService.registerPassenger({body: this.form.value})
      .subscribe();
  }
}
