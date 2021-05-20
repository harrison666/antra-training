import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  // base services we can use, other services can talk through this servics
  // HttpClient => used to communicate with API
  headers: HttpHeaders | undefined;
  constructor(protected http: HttpClient) {}

  // get array of JSON objects
  getList(path: string): Observable<any[]> {
    return this.http
      .get(`${environment.apiUrl}${path}`)
      .pipe(map((resp) => resp as any[]));
  }

  // get single jSON object
  getOne(path: string, id?: number): Observable<any> {
    return this.http
      .get(`${environment.apiUrl}${path}` + id)
      .pipe(map((response) => response));
  }

  // post something
  create(path: string, resource: any, options?: any): Observable<any> {
    return this.http
      .post(`${environment.apiUrl}${path}`, resource, { headers: this.headers })
      .pipe(map((response) => response));
  }

  // PUT
  update(path: string, resource: any) {
    return this.http
      .put(
        `${environment.apiUrl}${path}`, resource)
      .pipe(map((response) => response));
  }

  // delete something
  delete(path: string, id?: number) {
    console.log("inside api Service");
    return this.http
      .delete(`${environment.apiUrl}${path}` + id).pipe(map((response) => response));
  }
}
