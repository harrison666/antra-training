import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';
import { Login } from 'src/app/shared/models/login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  userLogin: Login = {
    email: '', password: ''
  }
  invalidLogin: boolean | undefined; 
  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    console.log(this.userLogin);
  }

  login(){
    console.log('button was clicked');
    // call the login method in auth service
    this.authService.login(this.userLogin).subscribe(
      response => {
        if (response) {
          // if auth service return true, redirect to home page
          this.router.navigate(['/']);
          // 2XX
        }
        else {
          // stay on the same page and show some error message saying un/pw is invalid
          this.invalidLogin = true;
        }
      },
      (err: any) => {
        (this.invalidLogin = true), console.log(err);
      }
    )
  }
  // simply observing two way binding, just for testing, remove it later
  get twoWayBindingInfo() { return JSON.stringify(this.userLogin) }

}
