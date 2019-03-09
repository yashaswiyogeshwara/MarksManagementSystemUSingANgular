import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'undefined',
    'Authorization': 'my-auth-token',
    'Access-Control-Allow-Credentials': 'true',
    'Access-Control-Allow-Origin': 'http://localhost:4200'
  })
};
@Injectable({
  providedIn: 'root'
})
export class AddServiceService {
  Url: string = "https://localhost:44365/api/exceldata/AddStudents"

  constructor(public http : HttpClient) { }
  AddStudents(fileToUpload: File):Observable<any>{
    debugger;
  const _formData = new FormData();
  _formData.append('_file', fileToUpload, fileToUpload.name);
  return this.http.post(this.Url, _formData);
    //return this.http.post(this.Url,path , httpOptions);
  }
}
