import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators'
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUel = 'http://localhost:5000/api/auth/';

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post(this.baseUel + 'login', model).pipe(
      map((response: any) => {
        const user = response;
        if (user) { localStorage.setItem('token', user.token); }
      }))
  }

  register(model:any){
return this.http.post(this.baseUel + 'register', model);
  }
}
