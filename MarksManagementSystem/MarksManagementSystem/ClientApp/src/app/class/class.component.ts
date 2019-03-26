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
  classInfo: ClassProfile[];
  gridData: any;
ClassForm:FormGroup;
Message: string;
  // use ng for and add data[0][4] sybjectnames to displaycolumns
  constructor(public classServicesService :ClassServicesService) { }
  displayedColumns: string[] = [];
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
          me.createGridData(data.data);
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
  /*
public class ClassProfile
    {
        public ClassProfile()
        {
        }
        public string HallTicket { get; set; }
        public double Average { get; set; }
        public int NoOfBacklogs { get; set; }
        public int NAAC { get; set; }

        public List<StudentMarks> StudentMarks { get; set; }
    }

public class StudentMarks
    {
        
  public string Hallticket { get; set; }
        public string SubjectName { get; set; }
        public string SubjectCode { get; set; }
        public int Year { get; set; }
        public int Sem { get; set; }
        public string Grade { get; set; }
        public int GradePoint { get; set; }

    }
*/
  public createGridData(data) {
    debugger;
    let me = this, headerData = {};
    me.gridData = [];
    if (data != null) {
      for (let i = 0; i < data.length; i++) {
        debugger;
        let rowData = { 'HallTicket': data[i]['hallTicket'], 'Average': data[i]['average'], 'NoOfBacklogs': data[i]['noOfBacklogs'], 'NAAC': data[i]['naac'] };
        let rsm = data[i]['studentMarks'];
        for (let k = 0; k < rsm.length; k++) {
          let key = rsm[k]['subjectName'];
          let value = rsm[k]['gradePoint'];
          rowData[key] = value;
        }
        me.gridData[i] = rowData;
      }
    }//break it furthur and add a property with Subject name and value as Grade point

    for (var h in me.gridData[0]) {
      me.displayedColumns.push(h);
    }
    debugger;
  }
 
}
