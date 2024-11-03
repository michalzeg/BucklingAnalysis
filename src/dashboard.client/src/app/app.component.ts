import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { connectSignalR, loadTrackingNumber } from './store/actions';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  constructor(private readonly store: Store) { }

  ngOnInit() {
    this.store.dispatch(loadTrackingNumber());
    this.store.dispatch(connectSignalR());
  }
}
