import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AddRoutingModule } from './add-routing.module';
import { AddStudentComponent } from './add-student/add-student.component';
import { AddMarksComponent } from './add-marks/add-marks.component';

@NgModule({
  declarations: [AddStudentComponent, AddMarksComponent],
  imports: [
    CommonModule,
    AddRoutingModule
  ]
})
export class AddModule { }
