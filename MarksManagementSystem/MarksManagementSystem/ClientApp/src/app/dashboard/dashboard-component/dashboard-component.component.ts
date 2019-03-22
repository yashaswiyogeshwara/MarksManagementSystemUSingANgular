//https://demos.creative-tim.com/material-dashboard/docs/2.1/getting-started/introduction.html
import { Component, OnInit } from '@angular/core';
import { RequestService } from 'src/app/Request.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-dashboard-component',
  templateUrl: './dashboard-component.component.html',
  styleUrls: ['./dashboard-component.component.css']
})
export class DashboardComponentComponent implements OnInit {

  constructor(public requestService:RequestService,public router : Router) { }

  ngOnInit() {
  }

}
