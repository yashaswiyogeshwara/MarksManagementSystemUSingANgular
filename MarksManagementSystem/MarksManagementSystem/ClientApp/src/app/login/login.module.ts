import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ReactiveFormsModule} from '@angular/forms';

import { LoginRoutingModule } from './login-routing.module';
import { LoginPageComponent } from './login-page/login-page.component';
import { RouterModule, Routes } from '@angular/router';
import { SignUpPageComponent } from '../sign-up/sign-up-page/sign-up-page.component';

const routes: Routes = [
  
  { path: 'Signup', component: SignUpPageComponent },
  
];
@NgModule({
  declarations: [LoginPageComponent],
  imports: [
    CommonModule,
    LoginRoutingModule,
    ReactiveFormsModule,
    [ RouterModule.forRoot(routes) ]
  
    
  ],
  exports:[
    LoginPageComponent,
    
  ]

})
export class LoginModule { }
