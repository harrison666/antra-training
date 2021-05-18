import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
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
    return this.http.get(`${environment.apiUrl}${path}`).pipe(map(resp => resp as any[]))
    .pipe();
  }

  // get single jSON object
  getOne(path: string, id?: number): Observable<any> {
    let url = `${environment.apiUrl}${path}` + id;
    return this.http.get(url).pipe(map(resp => resp as any))
  }

  // post something
  create(path: string, resource: any, options?: any): Observable<any> {
    return this.http
      .post(`${environment.apiUrl}${path}`, resource, { headers: this.headers })
      .pipe(map((response) => response));
  }

  // PUT
  update() {}

  // delete somethign
  delete() {}
}
