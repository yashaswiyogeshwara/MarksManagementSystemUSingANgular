import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponentComponent } from './dashboard-component/dashboard-component.component';
import { RouterModule, Routes } from '@angular/router';
import { AddStudentComponent } from '../add/add-student/add-student.component';
import { AddMarksComponent } from '../add/add-marks/add-marks.component';
import{AddModule} from'../add/add.module';
import{StudentProfileModule} from '../student-profile/student-profile.module';
import{StudentProfileComponent} from'../student-profile/student-profile/student-profile.component';
import{ClassModule} from '../class/class.module';
import{ClassComponent} from'../class/class.component';
import{DepartmentModule} from '../department/department.module';
import {DepartmentComponentComponent} from '../department/department-component/department-component.component'



const routes: Routes = [
  
  {path:'AddStudent',component:AddStudentComponent},
  {path:'AddMarks',component:AddMarksComponent},
  {path:'StudemtProfile',component:StudentProfileComponent},
  {path:'Class',component:ClassComponent},
    {path:'Department',component:DepartmentComponentComponent}

];

@NgModule({
  declarations: [DashboardComponentComponent],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    AddModule,
    StudentProfileModule,
     ClassModule,
    DepartmentModule,
    [ RouterModule.forRoot(routes) ]

   
  ],
  exports:[
    DashboardComponentComponent, 
  ]
  
})
export class DashboardModule { }
