import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Component } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { LoginModule } from './login/login.module';
import {SignUpModule} from './sign-up/sign-up.module';
import { RouterModule, Routes } from '@angular/router';
import { LoginPageComponent } from './login/login-page/login-page.component';
import { SignUpPageComponent } from './sign-up/sign-up-page/sign-up-page.component';
import{DashboardModule} from'./dashboard/dashboard.module';
import{AddModule} from'./add/add.module';
import { DashboardComponentComponent } from './dashboard/dashboard-component/dashboard-component.component';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { from } from 'rxjs';
import { AddStudentComponent } from './add/add-student/add-student.component';
import { AddMarksComponent } from './add/add-marks/add-marks.component';




const routes: Routes = [
  { path: '', component:LoginPageComponent },
  {path:'Signup',component:SignUpPageComponent},
  {path:'Dashboard',component:DashboardComponentComponent},
  {path:'AddStudent',component:AddStudentComponent},
  {path:'AddMarks',component:AddMarksComponent},
];

@NgModule({
  declarations: [
    AppComponent,
    
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    AppRoutingModule,
    LoginModule,
    HttpClientModule,
    SignUpModule,
    DashboardModule ,
    AddModule,
    [ RouterModule.forRoot(routes), BrowserAnimationsModule],
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
