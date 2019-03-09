import { Component, OnInit, ViewChild } from '@angular/core';
import { AddServiceService } from '../add-service.service';

@Component({
  selector: 'app-add-student',
  templateUrl: './add-student.component.html',
  styleUrls: ['./add-student.component.css']
})
export class AddStudentComponent implements OnInit {

  @ViewChild('fileDialog') fileDialog : Element
  constructor(public addService : AddServiceService) { }

  ngOnInit() {
  }

  public fileUplode():void{
    
  }

  public OpenFileDialog():void{
    debugger;
    let event = new MouseEvent('click', {bubbles: false});
    this.fileDialog['nativeElement'].dispatchEvent(event);
  }

  public FileSelect($event: any){
    debugger;
    const fileSelected: File = $event.target.files[0];
   this.addService.AddStudents(fileSelected)
   .subscribe( (response) => {
      console.log('set any success actions...');
      return response;
    },(error) => {
       console.log('set any error actions...');
     });
    
  }

}
