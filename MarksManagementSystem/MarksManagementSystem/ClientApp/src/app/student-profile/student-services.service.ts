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
export class StudentServicesService {

  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) { }

  public getStudent(hallTicketNo: string): Observable<any> {
    debugger;
    let me = this;
    let params = new HttpParams();
    params.set('hallTicketNo', hallTicketNo['HallticketNumber']);
    //me.baseUrl + "api/ValuesController/GetStudents?hallTicketNo=" + hallTicketNo['HallticketNumber']
    return me.http.get(me.baseUrl + "api/Values/GetStudents?hallTicketNo=" + hallTicketNo['HallticketNumber']);
  } 

}
