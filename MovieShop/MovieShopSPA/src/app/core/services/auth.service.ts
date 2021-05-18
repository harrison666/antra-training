import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Login } from 'src/app/shared/models/login';
import { User } from 'src/app/shared/models/user';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  // can be used only inside this class to push data
  private currentUserSubject = new BehaviorSubject<User>({} as User);
  // public here so that any component can subscribe to this subject and get the data
  public currentUser = this.currentUserSubject.asObservable();

  private isAuthSubject = new BehaviorSubject<boolean>(false);
  // header component can use this subject to check whether use is authticated so that if tru hide login/register buttons in header
  public isAuth = this.isAuthSubject.asObservable();

  private user!: User;

  constructor(private apiService: ApiService) {}

  // take un/pw from login component and post it to API service that will post to API
  login(userLogin: Login): Observable<boolean> {
    return this.apiService.create('account/login', userLogin).pipe(
      map((response) => {
        if (response) {
          // save it to local storage
          localStorage.setItem('token', response.token);
          this.populateUserInfo();
          return true;
        }
        return false;
      })
    );
  }

  populateUserInfo() {
    // get the token from localstoarge and decode it and get the user info such as email, firt name, lastname etc to show in the header

    var token = localStorage.getItem('token');
    if (token) {
      // decode the token

      var decodedToken = new JwtHelperService().decodeToken(token);
      this.user = decodedToken;

      // pushing data to the observables
      this.currentUserSubject.next(this.user);
      this.isAuthSubject.next(true);
    }
  }

  logout() {
    // remove the token from local storage and change the subjects to default values
    localStorage.removeItem('token');
    this.currentUserSubject.next({} as User);
    this.isAuthSubject.next(false);
  }
}