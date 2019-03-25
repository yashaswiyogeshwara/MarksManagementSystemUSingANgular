import { Component, OnInit } from '@angular/core';
import {FormGroup,FormControl} from '@angular/forms';
import{AddSubjectServicesService} from '../add-subject-services.service';
@Component({
  selector: 'app-add-subject-component',
  templateUrl: './add-subject-component.component.html',
  styleUrls: ['./add-subject-component.component.css']
})
export class AddSubjectComponentComponent implements OnInit {
  AddSubjectForm:FormGroup;
  Message: string;

  constructor(public addSubjectServicesService:AddSubjectServicesService) { }
  ngOnInit() {
    this.AddSubjectForm=new FormGroup({
      Department:new FormControl,
      Year:new FormControl,
      Semester:new FormControl,
      SubjectName:new FormControl,
      SubjectCode:new FormControl,
    });
  }

public  AddSubject():void{
  debugger;
  let me = this;
console.log(this.AddSubjectForm);
if(me.AddSubjectForm.value){
this.addSubjectServicesService.addSubject(this.AddSubjectForm.get('Department').value,this.AddSubjectForm.get('Year').value,this.AddSubjectForm.get('Semester').value,this.AddSubjectForm.get('SubjectName').value,this.AddSubjectForm.get('SubjectCode').value).subscribe((data) => {
if(data.success==true)
{
  alert("Succesful added data");
}
else
{
  alert("data not entered");
}
});
}
}

}