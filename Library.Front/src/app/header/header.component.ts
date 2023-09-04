import { Component, OnInit, inject } from '@angular/core';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit{
  auth = inject(AuthService);
  username!: string;

  ngOnInit(): void {
    if(!this.auth.isLogged())
      return;
    this.auth.getUsername().subscribe((res)=>{
      this.username = res;
    },(error)=>{
      this.auth.logout();
    });
  }
}
