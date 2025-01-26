import { Component } from '@angular/core';
import { PassengerService } from "../api/services/passenger.service";
import {FormsModule, ReactiveFormsModule, FormBuilder, Validators} from "@angular/forms";
import { AuthService } from "../auth/auth.service";

@Component({
  selector: 'app-register-passenger',
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './register-passenger.component.html',
  standalone: true,
  styleUrl: './register-passenger.component.css'
})
export class RegisterPassengerComponent {
  constructor(private passengerService: PassengerService, private fb: FormBuilder, private auth: AuthService) {}

  form = this.fb.group({
    email: [''],
    firstName: [''],
    lastName: [''],
    isFemale: [true]
  })

  checkPassenger(): void {
    const params = { email: this.form.get('email')?.value }
    this.passengerService.findPassenger(params).subscribe(_ => {
      this.auth.loginUser({ email: this.form.get('email')?.value} )
    })
  }

  register() {
    this.passengerService.registerPassenger({body: this.form.value})
      .subscribe(_ => this.auth.loginUser({ email: this.form.get('email')?.value }),
        console.error)
  }
}
