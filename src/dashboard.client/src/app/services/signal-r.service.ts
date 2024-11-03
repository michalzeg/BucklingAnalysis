import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { environment } from '../../environments/environment';
import * as actions from '../store/actions';
import { Store } from '@ngrx/store';
import { ActivityType } from '../shared/types/activity-type';
import { CalculixProgress } from '../shared/models/calculix-progress';
import { Geometry } from '../shared/models/geometry';

const url = `${environment.baseUrl}/dashboardHub`;

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: HubConnection | undefined;

  constructor(private readonly store: Store) { }

  async startConnection(): Promise<void> {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(url, { withCredentials: false })  // Adjust the URL to match your backend
      .withAutomaticReconnect()
      .configureLogging(LogLevel.Debug)
      .build();

    try {
      await this.hubConnection.start()

      this.hubConnection.on('reportCalculixProgress', (progress: CalculixProgress) => {
        this.store.dispatch(actions.setCalculixProgess({ progress }));
      });

      this.hubConnection.on('activityCompleted', (activityType: ActivityType) => {
        this.store.dispatch(actions.setActivityCompleted({ activityType }))
      });

      this.hubConnection.on('facadeGenerated', () => {
        this.store.dispatch(actions.downloadFacade());
      });

      this.hubConnection.on('linearAnalysisFinished', () => {
        this.store.dispatch(actions.downloadLinearAnalysisResults());
      });

      this.hubConnection.on('bucklingAnalysisFinished', () => {
        this.store.dispatch(actions.downloadBucklingAnalysisResults());
      });

      this.hubConnection.on('nonLinearAnalysisFinished', () => {
        this.store.dispatch(actions.downloadNonLinearAnalysisResults());
      });

    }
    catch (err) {
      console.error('Error while starting connection: ' + err);
    }
  }

  async startCalculations(geometry: Geometry): Promise<string> {
    const trackingNumber = await this.hubConnection?.invoke<string>('startCalculations', geometry)
    return trackingNumber ?? '';
  }

  async reconnect(trackingNumber: string): Promise<void> {
    await this.hubConnection?.invoke('reconnect', trackingNumber);
  }
}
