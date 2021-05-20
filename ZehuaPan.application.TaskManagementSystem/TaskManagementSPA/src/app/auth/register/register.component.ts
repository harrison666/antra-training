import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/core/services/user.service';
import { UserRequest } from 'src/app/shared/model/userRequest';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  user: UserRequest = {
    email: '',
    password: '',
    fullname: '',
    mobileno: '',
  };
  constructor(private userService: UserService, private router: Router) {}

  ngOnInit(): void {

  }

  register() {
    console.log('button was clicked');
    this.userService.addUser(this.user).subscribe((response) => {
      if (response) {
        this.router.navigate(['/']);
      }
    });
  }

  // simply observing two way binding, just for testing, remove it later
  get twoWayBindingInfo() {
    return JSON.stringify(this.user);
  }
}
