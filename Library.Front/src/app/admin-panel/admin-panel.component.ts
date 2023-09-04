import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { HeaderComponent } from '../header/header.component';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit{
  constructor(private router: Router, private auth: AuthService){
    const token = localStorage.getItem("session_token");
    if(token === null){
      this.router.navigate(['login']);
    }

    
  }
  ngOnInit(): void {
    this.auth.getUsername().subscribe(
      (res)=>{
        this.user = res;
      },
      (error)=>{
        console.log(error);
        this.auth.logout()
      }
    )
  }

  user!: string;
}
