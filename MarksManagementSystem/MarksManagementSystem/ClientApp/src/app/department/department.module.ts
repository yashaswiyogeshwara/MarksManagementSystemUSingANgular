import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule,ReactiveFormsModule } from '@angular/forms'; 
import {MatTableModule} from '@angular/material/table';

import { DepartmentRoutingModule } from './department-routing.module';
import { DepartmentComponentComponent } from './department-component/department-component.component';

@NgModule({
  declarations: [DepartmentComponentComponent],
  imports: [
    CommonModule,
    DepartmentRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    MatTableModule
  ]
})
export class DepartmentModule { }
