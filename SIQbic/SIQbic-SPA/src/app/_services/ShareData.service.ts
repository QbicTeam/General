import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { DataSource } from '../_model/DataSource';

@Injectable({
  providedIn: 'root'
})
export class ShareDataService {

  private actionSource = new BehaviorSubject<string>("");
  currentActionSource = this.actionSource.asObservable();

  private companySource = new BehaviorSubject<DataSource>(null);
  companyDataSource = this.companySource.asObservable();

  constructor() { }

  notifyActionSource(action: string) {
    this.actionSource.next(action);
  }

  notifyCompanyDataSource(data: DataSource) {
    this.companySource.next(data);
  }

}
