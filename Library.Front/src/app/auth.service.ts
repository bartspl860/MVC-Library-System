import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginModel } from './model/Login/login-model';
import { Observable } from 'rxjs';
import { LoginResponse } from './model/Login/login-response';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  
  private apiUrl = "http://localhost:64670/api/authorize/";
  constructor(private http: HttpClient) { }

  login(credentials: LoginModel) : Observable<LoginResponse>{
    return this.http.post<LoginResponse>(this.apiUrl + "login", credentials);
  }

}
