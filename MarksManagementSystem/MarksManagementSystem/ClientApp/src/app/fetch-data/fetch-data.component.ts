import { Component, Inject, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];
  @ViewChild('fileDialog') fileDialog: Element;
  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {
    http.get<WeatherForecast[]>(baseUrl + 'api/SampleData/WeatherForecasts').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }

  public fileUplode(): void {

  }

  public OpenFileDialog(): void {
    debugger;
    let event = new MouseEvent('click', { bubbles: false });
    this.fileDialog['nativeElement'].dispatchEvent(event);
  }

  public FileSelect($event: any) {
    debugger;
    const fileSelected: File = $event.target.files[0];
    this.AddStudents(fileSelected)
      .subscribe((response) => {
        console.log('set any success actions...');
        return response;
      }, (error) => {
        console.log('set any error actions...');
      });

  }

  public AddStudents(fileToUpload: File): Observable<any> {
    debugger;
    const _formData = new FormData();
    _formData.append('_file', fileToUpload, fileToUpload.name);
    return this.http.post(this.baseUrl + 'api/ExcelData/AddStudents', _formData);
    //return this.http.post(this.Url,path , httpOptions);
  }

}

interface WeatherForecast {
  dateFormatted: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
