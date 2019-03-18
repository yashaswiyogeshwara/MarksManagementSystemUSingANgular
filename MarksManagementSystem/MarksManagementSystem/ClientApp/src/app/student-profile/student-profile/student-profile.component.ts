import { Component, OnInit } from '@angular/core';
import {FormGroup,FormControl} from '@angular/forms';
import { StudentServicesService } from '../student-services.service';

import { StudentProfile } from '../Models/StudentsProfile';



@Component({
  selector: 'app-student-profile',
  templateUrl: './student-profile.component.html',
  styleUrls: ['./student-profile.component.css']
})
export class StudentProfileComponent implements OnInit {
  StudentForm: FormGroup;
  StudentData: StudentProfile;
  Message: string;
  constructor(public studentServicesService :StudentServicesService) { }
  displayedColumns: string[] = ['SubjectName', 'SubjectCode', 'Grade','GradePoint'];
  ngOnInit() {
    this.StudentForm=new FormGroup({
      HallticketNumber:new FormControl,
     
    });
  }
  public SendHallticket(): void{
    let me = this;
    console.log(this.StudentForm.value);
    if (this.StudentForm.value) {
      this.studentServicesService.getStudent(this.StudentForm.value).subscribe((data) => {
        if (data && data.data) {
          me.Message = null;
          me.StudentData = data.data;
          console.log( me.StudentData);
        } else {
          me.Message = "This Student does not have any Marks data, please verify the Hallticket and Renter";
        }
      });
    }
  }
 
}
