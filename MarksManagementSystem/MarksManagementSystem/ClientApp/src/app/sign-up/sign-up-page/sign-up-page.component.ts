import { Component, OnInit } from '@angular/core';
import {FormGroup,FormControl} from '@angular/forms';
import { SingupService } from '../Services/singup.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-sign-up-page',
  templateUrl: './sign-up-page.component.html',
  styleUrls: ['./sign-up-page.component.css']
})
export class SignUpPageComponent implements OnInit {
  SignupForm:FormGroup;
  ErrorMessage : string;
  constructor(public singUpService : SingupService, public router : Router) { 
   
  }

  ngOnInit() {
    this.SignupForm=new FormGroup({
      Name:new FormControl,
      Password:new FormControl,
      Email:new FormControl,
      IsAdmin:new FormControl
    });

  }
  
  public onSubmit(): void{
    debugger;
    
    const me = this;
    if(this.SignupForm.get('IsAdmin').value==null){
      this.SignupForm.controls['IsAdmin'].setValue(false);
    }
    
   this.singUpService.RegisterUser(this.SignupForm.value).subscribe((data)=>{
    if(data['success']){
      me.ErrorMessage = null;
      me.router.navigate(['']);
    } else {
      me.ErrorMessage = "Error while signing up the data, please try with a different user name";  
    }
   }, (error) => {
    me.ErrorMessage = "Error while signing up the data";
   });
  }
}
