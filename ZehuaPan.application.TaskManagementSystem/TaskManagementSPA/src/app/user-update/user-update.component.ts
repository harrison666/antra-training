import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../core/services/user.service';
import { UserRequest } from '../shared/model/userRequest';

@Component({
  selector: 'app-user-update',
  templateUrl: './user-update.component.html',
  styleUrls: ['./user-update.component.css']
})
export class UserUpdateComponent implements OnInit {

  user: UserRequest = {
    email: '',
    password: '',
    fullname: '',
    mobileno: '',
  };
  id!: number;

  constructor(private userService: UserService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      console.log(params);
      this.id = +params.get('id')!;
    });
  }

  update() {
    this.userService.updateUser(this.id, this.user).subscribe((response) => {
      if (response) {
        this.router.navigate(['/']);
      }
    });
  }

}
