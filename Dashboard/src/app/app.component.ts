import { Component, OnDestroy, OnInit } from '@angular/core';
import { DataService } from './data.service';
import { Subscription, interval, startWith, switchMap } from 'rxjs';
import { MatTableDataSource } from '@angular/material/table';

export interface CarData {
  car_id: string;
  speed: number;
  timestamp: number;
}
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
  constructor(private dataservice: DataService) { 
    this.timeInterval = interval(1000)
    .pipe(
      startWith(0),
      switchMap(() => this.dataservice.GetAlloverspeeding())
    ).subscribe(x => { if(x.length > 0) this.dataSource.data = x.sort(s => s.timestamp)})
    this.speedtimeInterval = interval(1000)
    .pipe(
      startWith(0),
      switchMap(() => this.dataservice.GetAlldata())
    ).subscribe(x => this.TotalMessages = x)
  }

  timeInterval: Subscription;
  speedtimeInterval: Subscription;
  TotalMessages = 0;
  title = 'DashBoard';
  displayedColumns: string[] = ['car_id', 'speed', 'timestamp'];
  dataSource = new MatTableDataSource<CarData>();
  ngOnInit(): void {

  }
  ngOnDestroy(): void {
    this.timeInterval.unsubscribe();
    this.speedtimeInterval.unsubscribe();
  }
}
