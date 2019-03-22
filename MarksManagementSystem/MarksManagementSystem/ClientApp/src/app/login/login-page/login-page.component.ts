import { Component, OnInit } from '@angular/core';
import {FormGroup,FormControl} from '@angular/forms';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginService } from './Services/login.service';
import { RequestService } from 'src/app/Request.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {
  employeeForm:FormGroup;
  ErrorMessage: string;
  constructor(public router: Router, public loginService: LoginService, public requestService: RequestService) {
    this.requestService.setAuthorizationToken("unauth");
   }

  ngOnInit() {
    this.employeeForm=new FormGroup({
      UserName: new FormControl,
      Password: new FormControl
    });
  }

  onSubmit(): void {
   let me = this;
   me.loginService.login(me.employeeForm.value).subscribe((data)=>{
    if(data['success']){
      me.ErrorMessage = null;
      me.requestService.setAuthorizationToken(data.token);
      me.router.navigate(['Dashboard']);
    } else {
      me.ErrorMessage = "Entered User Name or Password is wrong.";  
    }
   }, (error) => {
    me.ErrorMessage = "Error while logging into the system";
   });
  }
}
