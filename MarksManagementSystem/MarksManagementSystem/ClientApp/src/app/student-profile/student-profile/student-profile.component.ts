import { Component, OnInit } from '@angular/core';
import {FormGroup,FormControl} from '@angular/forms';
import { StudentServicesService } from '../student-services.service';
import {student} from'../MockStudentData';
import { StudentData } from 'src/app/Models/StudentData';


@Component({
  selector: 'app-student-profile',
  templateUrl: './student-profile.component.html',
  styleUrls: ['./student-profile.component.css']
})
export class StudentProfileComponent implements OnInit {
  StudentForm:FormGroup;
  constructor(public studentServicesService :StudentServicesService) { }
  //StudentData=student;
  Student = student;
  SelectStudent:StudentData;
  ngOnInit() {
    this.StudentForm=new FormGroup({
      HallticketNumber:new FormControl,
     
    });
  }
  public  SendHallticket():void{
    console.log(this.StudentForm.value);
    
    
  }
  onSelect(hero: StudentData): void {
    this.SelectStudent = hero;
  }
}
