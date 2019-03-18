import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ClassRoutingModule } from './class-routing.module';
import { ClassComponent } from './class.component';
import {MatTableModule} from '@angular/material/table';
import { FormsModule,ReactiveFormsModule } from '@angular/forms'; 



@NgModule({
  declarations: [ClassComponent],
  imports: [
    CommonModule,
    ClassRoutingModule,
    MatTableModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class ClassModule { }
