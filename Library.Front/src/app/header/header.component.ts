import { Component, inject } from '@angular/core';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

  auth = inject(AuthService);

  isLogged(): boolean{
    return localStorage.getItem("session_token") !== null;
  }

  logout(){
    this.auth.logout();
  }
}
