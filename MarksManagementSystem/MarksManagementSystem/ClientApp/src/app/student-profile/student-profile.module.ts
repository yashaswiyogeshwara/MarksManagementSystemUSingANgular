import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ReactiveFormsModule} from '@angular/forms';
import { FormsModule } from '@angular/forms'; 

import {MatTableModule} from '@angular/material/table';

import { StudentProfileRoutingModule } from './student-profile-routing.module';
import { StudentProfileComponent } from './student-profile/student-profile.component';

@NgModule({
  declarations: [StudentProfileComponent],
  imports: [
    CommonModule,
    StudentProfileRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    MatTableModule
  ]
})
export class StudentProfileModule { }
