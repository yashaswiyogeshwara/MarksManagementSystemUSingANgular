import { Component, OnInit } from '@angular/core';
import {MockClassData} from'./MockClassData';
import { ClassData } from 'src/app/Models/ClassData';


@Component({
  selector: 'app-class',
  templateUrl: './class.component.html',
  styleUrls: ['./class.component.css']
})
export class ClassComponent implements OnInit {
classdata:ClassData[];
  constructor() { }
  displayedColumns: string[] = ['Sno', 'HallTicketNumber', 'Avearage', 'NoOfBacklogs','NAACBacklogs'];
  ngOnInit() {
  }
  public  SendClass():void{
    
    this.classdata = MockClassData;
  }
 
}
