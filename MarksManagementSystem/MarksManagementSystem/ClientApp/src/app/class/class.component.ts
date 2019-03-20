import { Component, OnInit } from '@angular/core';
import {MockClassData} from'./MockClassData';
import { ClassData } from 'src/app/Models/ClassData';
import {FormGroup,FormControl} from '@angular/forms';
import {  ClassServicesService} from './class-services.service';
import{ClassProfile} from './Models/ClassProfile';
import { from } from 'rxjs';
@Component({
  selector: 'app-class',
  templateUrl: './class.component.html',
  styleUrls: ['./class.component.css']
})
export class ClassComponent implements OnInit {
classdata:ClassData[];
classProfile:ClassProfile;
classInfo:ClassProfile[];
ClassForm:FormGroup;
Message: string;
yearOfJoining: String;
YearOfJoining: FormControl;
      Department: FormControl;
      Year: FormControl;
      Semester: FormControl;
      Section: FormControl;
  constructor(public classServicesService :ClassServicesService) { }
  displayedColumns: string[] = ['HallTicketNumber', 'Avearage', 'NoOfBacklogs','NAAC'];
  ngOnInit() {
    this.ClassForm=new FormGroup({
     YearOfJoining:new FormControl,
      Department:new FormControl,
      Year:new FormControl,
      Semester:new FormControl,
      Section:new FormControl,

    });
    
  }
  public  SendClass():void{
    let me = this;
    console.log(this.ClassForm)
    this.classdata = MockClassData;
   console.log(this.ClassForm.get('YearOfJoining').value);
    if ( this.ClassForm.value) {
      this.classServicesService.getClass( this.ClassForm.get('YearOfJoining').value,this.ClassForm.get('Department').value,this.ClassForm.get('Year').value,this.ClassForm.get('Semester').value,this.ClassForm.get('Section').value).subscribe((data) => {
        if (data && data.data) {
          me.classProfile=data.data;
          console.log(  me.classProfile);
          me.Message = null;
         
        } else {
          me.Message = "This Student does not have any Marks data, please verify the Hallticket and Renter";
        }
      });
    }
  }
 
}
