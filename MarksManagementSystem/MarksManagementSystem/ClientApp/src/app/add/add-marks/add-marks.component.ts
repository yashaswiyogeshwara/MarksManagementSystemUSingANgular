import { Component, OnInit, ViewChild } from '@angular/core';
import { AddServiceService } from '../add-service.service';
@Component({
  selector: 'app-add-marks',
  templateUrl: './add-marks.component.html',
  styleUrls: ['./add-marks.component.css']
})
export class AddMarksComponent implements OnInit {
    @ViewChild('fileDialog') fileDialog : Element
  constructor(public addService : AddServiceService) { }
  Message: string;
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
   this.addService.AddMarks(fileSelected)
     .subscribe((response) => {
       this.Message = null;
       if (!response.success) {
         if (response.mess) {
           this.Message = response.mess;
         }
       }
      console.log('set any success actions...');
      return response;
    },(error) => {
       console.log('set any error actions...');
     });
    
  }

}
