import { Component, OnInit } from '@angular/core';
import { UserService } from '../core/services/user.service';
import { UserResponse } from '../shared/model/userReponse';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  users: UserResponse[] | undefined;
  constructor(private userService: UserService) {}

  ngOnInit(): void {
    console.log('inside Genres Component init method');
    this.fetchData();
  }

  fetchData() {
    this.users = [];
    this.userService.getAllUsers()
    .subscribe(
      u => {
        this.users = u;
      }
    )
  }

  delete(id: number) {
    console.log("inside delete method");
    this.userService.deleteUser(id).subscribe(() => {
      console.log("data deleted");
      this.fetchData();
    });
    ;
  }

}
