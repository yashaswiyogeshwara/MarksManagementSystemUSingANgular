import { Component, OnInit } from '@angular/core';
import {FormGroup,FormControl} from '@angular/forms';
import {  ClassServicesService} from '../class-services.service';
import { SubjectDataProfile } from '../Models/SubjectDataProfile';

@Component({
  selector: 'app-subject-component',
  templateUrl: './subject-component.component.html',
  styleUrls: ['./subject-component.component.css']
})
export class SubjectComponentComponent implements OnInit {
  ClassForm:FormGroup;
  subjectDataProfile:SubjectDataProfile;
i:number;
  Message: string;
  displayedColumnsSubject: string[] = ['HallTicketNumber', 'GradeInSubject', 'GradePointInSubject'];

  constructor(public classServicesService :ClassServicesService) { }

  ngOnInit() {

    this.ClassForm=new FormGroup({
      YearOfJoining:new FormControl,
       Department:new FormControl,
       Year:new FormControl,
       Semester:new FormControl,
       Section:new FormControl,
       SubjectName:new FormControl,
     });
     
  }
  public  SendSubject( any : any):void{
    let me = this;
    if(this.ClassForm && any==0 )
    {
      this.classServicesService.getSubject( this.ClassForm.get('YearOfJoining').value,this.ClassForm.get('Department').value,this.ClassForm.get('Year').value,this.ClassForm.get('Semester').value,this.ClassForm.get('Section').value,this.ClassForm.get('SubjectName').value).subscribe((data) => {
        if (data && data.data) {
        me.subjectDataProfile=data.data;    
          me.Message = null;
         
        } else {
          if (data.mess && data.mess.length > 0) {
            me.Message = data.mess;
          } else {
            me.Message = "This Subject does not have any Marks data, please verify the Hallticket and Renter";
          }
        }
      });
    }

    else if(this.ClassForm && any==1)
    {
      console.log("backlogs");
      debugger;
      this.classServicesService.getSubjectBacklogs( this.ClassForm.get('YearOfJoining').value,this.ClassForm.get('Department').value,this.ClassForm.get('Year').value,this.ClassForm.get('Semester').value,this.ClassForm.get('Section').value,this.ClassForm.get('SubjectName').value).subscribe((data) => {
        if (data && data.data) {
        me.subjectDataProfile=data.data;   
        console.log(me.subjectDataProfile); 
          me.Message = null;
         
        } else {
          if (data.mess && data.mess.length > 0) {
            me.Message = data.mess;
          } else {
            me.Message = "This Subject does not have any Marks data, please verify the Hallticket and Renter";
          }
        }
      });
    
    }
  }
}
