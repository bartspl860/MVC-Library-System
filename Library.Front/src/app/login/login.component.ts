import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { LoginModel } from '../model/Login/login-model';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  formBuilder: FormBuilder = inject(FormBuilder);
  loginForm! : FormGroup;

  auth: AuthService = inject(AuthService);
  router: Router = inject(Router);

  loginError: boolean = false;

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      login: ['', []],
      password: ['', []]
    });
  }

  constructor(){
    const token = localStorage.getItem("session_token");
    if(token !== null){
      this.router.navigate(['admin']);
    }
  }

  authorize(){
    const loginModel = new LoginModel(
      this.loginForm.value.login,
      this.loginForm.value.password
    );

    this.auth.login(loginModel).subscribe(
      (res) =>{
        localStorage.setItem("session_token", res.token);

        this.auth.getUsername().subscribe((res)=>{
          this.auth.username = res;
        })

        this.router.navigate(['admin']);
      },
      (error) =>{        
        this.loginError = true;
      }
    )
  }
}
