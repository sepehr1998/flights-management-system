import { Component } from '@angular/core';
import { PassengerService } from "../api/services/passenger.service";
import { FormsModule, ReactiveFormsModule, FormBuilder, Validators } from "@angular/forms";
import { AuthService } from "../auth/auth.service";
import { Router } from "@angular/router";

@Component({
  selector: 'app-register-passenger',
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './register-passenger.component.html',
  standalone: true,
  styleUrl: './register-passenger.component.css'
})
export class RegisterPassengerComponent {
  constructor(
    private passengerService: PassengerService,
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router) {}

  form = this.fb.group({
    email: ['', Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(100)])],
    firstName: ['', Validators.compose([Validators.required, Validators.minLength(2), Validators.maxLength(35)])],
    lastName: ['', Validators.compose([Validators.required, Validators.minLength(2), Validators.maxLength(35)])],
    isFemale: [true, Validators.required]
  })

  checkPassenger(): void {
    const params = { email: this.form.get('email')?.value || ""}
    this.passengerService.findPassenger(params).subscribe(
      this.login, error => {
        if(error.status !== 404) {
          console.error(error)
        }
      }
    )
  }

  register() {
    if(this.form.invalid)
      return
    this.passengerService.registerPassenger({body: this.form.value})
      .subscribe(this.login,
        console.error)
  }

  private login = () => {
    this.auth.loginUser({ email: this.form.get('email')?.value || ""})
    this.router.navigate(['/search-flights'])
  }
}
