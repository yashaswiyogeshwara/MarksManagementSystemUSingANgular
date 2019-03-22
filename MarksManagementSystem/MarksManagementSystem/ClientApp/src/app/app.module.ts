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
import{StudentProfileModule} from './student-profile/student-profile.module';
import{StudentProfileComponent} from'./student-profile/student-profile/student-profile.component';
import{ClassModule} from './class/class.module';
import{ClassComponent} from'./class/class.component';
import{DepartmentModule} from './department/department.module';
import {DepartmentComponentComponent} from './department/department-component/department-component.component'
import {MatTableModule} from '@angular/material/table';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { CommonInterceptor } from './Common-Interceptor';
import { RequestService } from './Request.service';

export const httpInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass : CommonInterceptor, multi: true }
];

const routes: Routes = [
  { path: '', component:LoginPageComponent },
  {path:'Signup',component:SignUpPageComponent},
  {path:'Dashboard',component:DashboardComponentComponent},
  {path:'AddStudent',component:AddStudentComponent},
  {path:'AddMarks',component:AddMarksComponent},
  {path:'StudemtProfile',component:StudentProfileComponent},
  {path:'Class',component:ClassComponent},
    {path:'Department',component:DepartmentComponentComponent}

  
  
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
    StudentProfileModule,
    ClassModule,
    DepartmentModule,
    MatTableModule,
    [ RouterModule.forRoot(routes), BrowserAnimationsModule],
  ],
  providers: [RequestService, httpInterceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
