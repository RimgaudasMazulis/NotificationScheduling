import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public schedules: Schedule[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Schedule[]>(baseUrl + 'api/notification-scheduling/all').subscribe(result => {
      this.schedules = result;
    }, error => console.error(error));
  }
}

interface Schedule {
  companyId: string;
  notifications: string[];
}
