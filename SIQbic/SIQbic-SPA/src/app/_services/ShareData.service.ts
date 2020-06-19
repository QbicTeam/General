import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { DataSource } from '../_model/DataSource';
import { QuickAction } from '../_model/QuickAction';

@Injectable({
  providedIn: 'root'
})
export class ShareDataService {

  private actionSource = new BehaviorSubject<QuickAction>(null);
  currentActionSource = this.actionSource.asObservable();

  private companySource = new BehaviorSubject<DataSource>(null);
  companyDataSource = this.companySource.asObservable();

  private liveNotificationSource = new BehaviorSubject<any>(null);
  liveNotificationChange = this.liveNotificationSource.asObservable();

  constructor() { }

  notifyActionSource(action: QuickAction) {
    this.actionSource.next(action);
  }

  notifyCompanyDataSource(data: DataSource) {
    this.companySource.next(data);
  }

  notifyLiveNotification(msg: any) {
    this.liveNotificationSource.next({action:"", msg: msg});
  }

}
