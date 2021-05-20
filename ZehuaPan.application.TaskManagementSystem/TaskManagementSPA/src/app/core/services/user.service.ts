import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserResponse } from 'src/app/shared/model/userReponse';
import { UserRequest } from 'src/app/shared/model/userRequest';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  // talk with movie API
  constructor(private apiService: ApiService) { }

  getUserById(id: number):Observable<UserResponse> {
    return this.apiService.getOne(`${'User/'}`, id);
  }

  getAllUsers(): Observable<UserResponse[]> {
    return this.apiService.getList(`${'User'}`);
  }

  addUser(user: UserRequest):Observable<UserResponse> {
    return this.apiService.create(`${'User/add'}`,user);
  }

  deleteUser(id: number) {
    console.log("inside userService");
    return this.apiService.delete(`${'User/delete/'}`, id);
  }

  updateUser(id: number, user:UserRequest) {
    return this.apiService.update(`${'User/update/'}`+ id, user);
  }


}
