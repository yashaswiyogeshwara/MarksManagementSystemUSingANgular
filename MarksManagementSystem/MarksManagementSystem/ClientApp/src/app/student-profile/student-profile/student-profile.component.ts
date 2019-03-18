import { Component, OnInit } from '@angular/core';
import {FormGroup,FormControl} from '@angular/forms';
import { StudentServicesService } from '../student-services.service';
import {student} from'../MockStudentData';
import { StudentData } from 'src/app/Models/StudentData';
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
        } else {
          me.Message = "This Student does not have any Marks data, please verify the Hallticket and Renter";
        }
      });
    }
  }
 
}
