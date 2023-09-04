import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginModel } from './model/Login/login-model';
import { Observable, catchError, of, throwError } from 'rxjs';
import { LoginResponse } from './model/Login/login-response';
import { Route, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  
  private apiUrl = "http://localhost:64670/api/authorize/";
  constructor(private http: HttpClient, private router: Router) { }

  username!: string;

  isLogged(){
    return localStorage.getItem("session_token") !== null;
  }

  login(credentials: LoginModel) : Observable<LoginResponse>{
    return this.http.post<LoginResponse>(this.apiUrl + "login", credentials);
  }

  logout(){
    localStorage.removeItem("session_token");
    this.router.navigate(['books']);
  }

  getUsername() : Observable<string>{
    let token = localStorage.getItem("session_token");

    console.log(token);
    const headers = this.createJwtHeader(token);

    return this.http.get(this.apiUrl + 'whoami', { headers, responseType: 'text' })
    .pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 200) {
          // Successful response with plain text (username)
          return of(error.error); // This will be the username
        } else {
          // Error response with JSON error message
          return throwError(error.error);
        }
      })
    );
  }

  createJwtHeader(token: string | null): HttpHeaders {
    // Set the Authorization header with the Bearer token
    const headers = new HttpHeaders().set("Authorization", "Bearer " + token);
  
    return headers;
  }
}
