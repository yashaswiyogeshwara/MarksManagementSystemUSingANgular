import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule,ReactiveFormsModule } from '@angular/forms'; 


import { AddSubjectRoutingModule } from './add-subject-routing.module';
import { AddSubjectComponentComponent } from './add-subject-component/add-subject-component.component';

@NgModule({
  declarations: [AddSubjectComponentComponent],
  imports: [
    CommonModule,
    AddSubjectRoutingModule,
    ReactiveFormsModule
  ]
})
export class AddSubjectModule { }
