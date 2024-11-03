import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, filter, map, switchMap, tap, withLatestFrom } from 'rxjs/operators';
import * as actions from '../actions';
import { Store } from '@ngrx/store';
import { AppState } from '../state';
import { from, of } from 'rxjs';
import { SignalRService } from '../../services/signal-r.service';
import { MessageService } from 'primeng/api';
import { selectGeometry } from '../selectors';

//kN/m2 => N/mm2
const unitFactor = 1000 / (1000 * 1000);

@Injectable()
export class SignalREffects {

  constructor(
    private actions$: Actions,
    private readonly store: Store<AppState>,
    private readonly signalRService: SignalRService,
    private readonly messageService: MessageService
  ) { }


  signalrConnect$ = createEffect(() => this.actions$.pipe(
    ofType(actions.connectSignalR),
    switchMap(() => from(this.signalRService.startConnection()).pipe(
      map(() => actions.reconnectSignalR()),
      catchError(() => {
        this.messageService.add({ severity: 'Error' });
        return of();
      }))
    ))
  );

  startCalculations$ = createEffect(() => this.actions$.pipe(
    ofType(actions.startCalculations),
    withLatestFrom(this.store.select(selectGeometry)),
    map(([, geometry]) => ({ ...geometry, uniformLoad: geometry.uniformLoad * unitFactor })),
    switchMap(geometry => from(this.signalRService.startCalculations(geometry)).pipe(
      tap(e => localStorage.setItem('trackingNumber', e)),
      map(e => actions.setTrackingNumber({ trackingNumber: e })),
      catchError(() => {
        this.messageService.add({ severity: 'Error' });
        return of(actions.emptyAction());
      })
    )
    ))
  );

  reconnect$ = createEffect(() => this.actions$.pipe(
    ofType(actions.reconnectSignalR),
    map(() => localStorage.getItem('trackingNumber')),
    filter(e => e !== null && e.trim() !== ''),
    switchMap(e => from(this.signalRService.reconnect(e!)).pipe(
      catchError(() => {
        this.messageService.add({ severity: 'Error' });
        return of();
      }))
    )), { dispatch: false }
  );

}


