import { Component, OnInit } from '@angular/core';
import {FormGroup,FormControl} from '@angular/forms';
import{DepartmentServicesService} from '../department-services.service'
import { from } from 'rxjs';
import{DepartmentData} from '../Models/DepartmentData'
@Component({
  selector: 'app-department-component',
  templateUrl: './department-component.component.html',
  styleUrls: ['./department-component.component.css']
})
export class DepartmentComponentComponent implements OnInit {
  DepartmentForm:FormGroup;
  Message: string;
  departmentData:DepartmentData;
  constructor(public departmentServicesService:DepartmentServicesService) { }
  displayedColumns: string[] = ['Section', 'Avearage'];

  ngOnInit() {
    this.DepartmentForm=new FormGroup({
      YearOfJoining:new FormControl,
       Department:new FormControl,
       Year:new FormControl,
       Semester:new FormControl,
 
     });
  }
  public  SendDepartment():void{
    debugger;
    let me = this;
    console.log(this.DepartmentForm.get('YearOfJoining').value);
    if ( this.DepartmentForm.value) {
      this.departmentServicesService.getDepartment(this.DepartmentForm.get('YearOfJoining').value,this.DepartmentForm.get('Department').value,this.DepartmentForm.get('Year').value,this.DepartmentForm.get('Semester').value).subscribe((data) => {
        if (data && data.data) {
          me.departmentData=data.data;
          console.log(data);
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
