import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})

export class FetchDataComponent {
  public schedules: Schedule[];
  public previousNotificationStepWidth: number;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Schedule[]>(baseUrl + 'api/notification-scheduling/all').subscribe(result => {
      this.schedules = result;
    }, error => console.error(error));

    this.previousNotificationStepWidth = 0;
  }

  calculateStepWidth(schedule: Schedule, index: number) {
    if (schedule.notifications !== undefined) {
      if (schedule.notifications[index - 1] !== undefined) {
        let minDate = this.getMinNotificationDate(schedule);
        let maxDate = this.getMaxNotificationDate(schedule);

        let firstSecondDifferenceInDays = this.calculateDaysDistanceForTwoNotifications(schedule, index -1, index);
        let minMaxDifferenceInDays = this.calculateDaysDistanceForTwoDates(minDate, maxDate);

        let width = Math.abs(firstSecondDifferenceInDays * 100 / minMaxDifferenceInDays);

        width += this.previousNotificationStepWidth;

        this.previousNotificationStepWidth = width;

        if (this.previousNotificationStepWidth === 100) {
          this.previousNotificationStepWidth = 0;
        }

        return Math.floor(width) - 5 + "%";
      }
    }
    return "0%";
  }

  getDaysDifferenceFromPreviousNotificationTillNotification(schedule: Schedule, index: number) {
    if (schedule.notifications !== undefined && schedule.notifications[index - 1] !== undefined) {
     return this.calculateDaysDistanceForTwoNotifications(schedule, index - 1, index) + " d.";
    }

    return null;
  }

  getClassColors(notification: string) {
    if (notification !== undefined) {
      let notificationDate = this.convertStringToDate(notification);

      if (notificationDate.getTime() <= new Date(Date.now()).getTime()) {
        return "one primary-color";
      }
      return "one no-color"; 
    }
  }

  getMinNotificationDate(schedule: Schedule) {
    if (schedule.notifications !== undefined) {
      let firstNotification = schedule.notifications[0];
      return this.convertStringToDate(firstNotification);
    }
  }

  getMaxNotificationDate(schedule: Schedule) {
    if (schedule.notifications !== undefined) {
      let lastNotification = schedule.notifications[schedule.notifications.length - 1];
      return this.convertStringToDate(lastNotification);
    }
  }

  calculateProgressPositionWidth(schedule: Schedule) {
    if (schedule.notifications !== undefined) {

      let minDate = this.getMinNotificationDate(schedule);
      let maxDate = this.getMaxNotificationDate(schedule);

      let minMaxDifferenceInDays = this.calculateDaysDistanceForTwoDates(minDate, maxDate);
      let minNowDifferenceInDays = this.calculateDaysDistanceForTwoDates(minDate, new Date(Date.now()));

      if (minNowDifferenceInDays < 0) {
        return "0%";
      }
      else if (minMaxDifferenceInDays === 0) {
        return "100%";
      }

      let width = Math.abs(minNowDifferenceInDays * 100 / minMaxDifferenceInDays);

      return Math.floor(width) + "%";
    }

    return "0%";
  }

  convertStringToDate(dateInString: string) {
    let parts = dateInString.split('/');
    return new Date(parseInt(parts[2]), parseInt(parts[1]) - 1, parseInt(parts[0]));
  }

  calculateDaysDistanceForTwoNotifications(schedule: Schedule, index1: number, index2: number) {
    if (schedule.notifications !== undefined) {
      let firstNotification = schedule.notifications[index1];
      let firstDate = this.convertStringToDate(firstNotification);

      let secondNotification = schedule.notifications[index2];
      let secondDate = this.convertStringToDate(secondNotification);

      return this.calculateDaysDistanceForTwoDates(firstDate, secondDate);
    }

    return 0;
  }

  calculateDaysDistanceFromNowTillLastNotification(schedule: Schedule) {
    if (schedule.notifications !== undefined) {
      let lastNotificationDate = this.getMaxNotificationDate(schedule);
      let days = this.calculateDaysDistanceForTwoDates(new Date(Date.now()), lastNotificationDate);

      if (days < 0) {
        return 0;
      }

      return Math.floor(days);
    }

    return 0;
  }
  
  calculateDaysDistanceForTwoDates(date1, date2) {
    var Difference_In_Time = date2.getTime() - date1.getTime();
    return Difference_In_Time / (1000 * 3600 * 24);
  }
}

interface Schedule {
  companyId: string;
  notifications: string[];
}
