import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
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
export class DepartmentServicesService {

  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) { }

  public getDepartment(yearOfJoining:string,department:string,year:string,semester:string): Observable<any> {
    debugger;
    let me = this;
    
    let data = new HttpParams()
    .set('YearOfJoining', yearOfJoining)
    .set('Department', department)
    .set('Year', year)
    .set('Semester', semester)
    
    //me.baseUrl + "api/ValuesController/GetStudents?hallTicketNo=" + hallTicketNo['HallticketNumber']
    return me.http.get(me.baseUrl + "api/Values/GetDepartment", {params: data});
    
  }
}
