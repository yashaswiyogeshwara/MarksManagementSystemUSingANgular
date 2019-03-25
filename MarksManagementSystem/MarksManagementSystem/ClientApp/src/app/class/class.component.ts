import { Component, OnInit } from '@angular/core';
import {MockClassData} from'./MockClassData';
import { ClassData } from 'src/app/Models/ClassData';
import {FormGroup,FormControl} from '@angular/forms';
import {  ClassServicesService} from './class-services.service';
import{ClassProfile} from './Models/ClassProfile';
import{SubjectDataProfile} from'./Models/SubjectDataProfile';
import { from } from 'rxjs';
@Component({
  selector: 'app-class',
  templateUrl: './class.component.html',
  styleUrls: ['./class.component.css']
})
export class ClassComponent implements OnInit {
classdata:ClassData[];
classProfile:ClassProfile;
subjectDataProfile:SubjectDataProfile;
classInfo:ClassProfile[];
ClassForm:FormGroup;
Message: string;

  constructor(public classServicesService :ClassServicesService) { }
  displayedColumns: string[] = ['HallTicketNumber', 'Avearage', 'NoOfBacklogs','NAAC'];
  displayedColumnsSubject: string[] = ['HallTicketNumber', 'GradeInSubject', 'GradePointInSubject'];
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
  public  SendClass():void{
    let me = this;
    console.log(this.ClassForm)
    if ( this.ClassForm.get('SubjectName').value==null) {
      this.classServicesService.getClass( this.ClassForm.get('YearOfJoining').value,this.ClassForm.get('Department').value,this.ClassForm.get('Year').value,this.ClassForm.get('Semester').value,this.ClassForm.get('Section').value).subscribe((data) => {
        if (data && data.data) {
          me.classProfile=data.data;
          console.log(  me.classProfile);
          me.Message = null;
         
        } else {
          if (data.mess && data.mess.length > 0) {
            me.Message = data.mess;
          } else {
            me.Message = "This Student does not have any Marks data, please verify the Hallticket and Renter";
          }
        }
      });
    }

  }
 
}
