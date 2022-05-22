import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

export class TimeZoneData {
  dateTime: string | undefined;
}

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  title = "Time Zone App";

  constructor(private http: HttpClient)
  {

  }

}
