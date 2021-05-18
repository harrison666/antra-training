import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/shared/models/user';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  isAuth!: boolean;
  user!: User;
  fullname: string | undefined;
  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.authService.isAuth.subscribe((data) => {
      this.isAuth = data;
      console.log(this.isAuth);
    });

    this.authService.currentUser.subscribe((data) => {
      this.user = data;
      this.fullname = data.given_name + " " + data.family_name;
      console.log(this.user.family_name);
    });
  }
}