import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponentComponent } from './dashboard-component/dashboard-component.component';
import { RouterModule, Routes } from '@angular/router';
import { AddStudentComponent } from '../add/add-student/add-student.component';
import { AddMarksComponent } from '../add/add-marks/add-marks.component';
import{AddModule} from'../add/add.module';



const routes: Routes = [
  
  {path:'AddStudent',component:AddStudentComponent},
  {path:'AddMarks',component:AddMarksComponent},
  
];

@NgModule({
  declarations: [DashboardComponentComponent],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    AddModule,
    [ RouterModule.forRoot(routes) ]

   
  ],
  exports:[
    DashboardComponentComponent, 
  ]
  
})
export class DashboardModule { }
