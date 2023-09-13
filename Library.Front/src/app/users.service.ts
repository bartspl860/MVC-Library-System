import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserResponse } from './model/User/user-response';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  private apiUrl = "http://localhost:64670/api/Users/";  

  constructor(private http: HttpClient) { }

  get(){
    let token = localStorage.getItem("session_token");
    let headers = AuthService.createJwtHeader(token);
    return this.http.get<UserResponse[]>(this.apiUrl, { headers });
  }

  delete(id: number){
    let token = localStorage.getItem("session_token");
    let headers = AuthService.createJwtHeader(token);
    return this.http.delete(this.apiUrl + id, { headers, responseType: 'text' });
  }
}
