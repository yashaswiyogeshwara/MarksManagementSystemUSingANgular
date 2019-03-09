import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ReactiveFormsModule} from '@angular/forms';

import { SignUpRoutingModule } from './sign-up-routing.module';
import { SignUpPageComponent } from './sign-up-page/sign-up-page.component';

@NgModule({
  declarations: [SignUpPageComponent],
  imports: [
    CommonModule,
    SignUpRoutingModule,
    ReactiveFormsModule
  ],
  exports:[
    SignUpPageComponent
  ]
})
export class SignUpModule { 

}
