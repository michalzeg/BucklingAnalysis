import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, filter, map, switchMap, tap, withLatestFrom } from 'rxjs/operators';
import * as actions from '../actions';
import { Store } from '@ngrx/store';
import { AppState } from '../state';
import { Observable, of } from 'rxjs';
import { MessageService } from 'primeng/api';
import { HttpService } from '../../services/http.service';
import { selectTrackingNumber } from '../selectors';
@Injectable()
export class HttpEffects {

  constructor(
    private actions$: Actions,
    private readonly store: Store<AppState>,
    private readonly httpService: HttpService,
    private readonly messageService: MessageService
  ) { }

  downloadFacade$ = createEffect(() => this.actions$.pipe(
    ofType(actions.downloadFacade),
    withLatestFrom(this.store.select(selectTrackingNumber)),
    map(([, trackingNumber]) => trackingNumber),
    switchMap(trackingNumber => this.httpService.getFacadeDetails(trackingNumber).pipe(
      this.matchTrackingNumber(trackingNumber),
      map(facade => actions.setFacade({ facade })),
      tap(() => this.messageService.add({ severity: 'success', summary: 'Facade ready' })),
      catchError(() => {
        this.messageService.add({ severity: 'Error' });
        return of();
      }))
    ))
  );

  downloadLinearAnalysisResults$ = createEffect(() => this.actions$.pipe(
    ofType(actions.downloadLinearAnalysisResults),
    withLatestFrom(this.store.select(selectTrackingNumber)),
    map(([, trackingNumber]) => trackingNumber),
    switchMap(trackingNumber => this.httpService.getLinearAnalysisResults(trackingNumber).pipe(
      this.matchTrackingNumber(trackingNumber),
      map(results => actions.setLinearAnalysisResults({ results })),
      tap(() => this.messageService.add({ severity: 'success', summary: 'Linear analysis results ready' })),
      catchError(() => {
        this.messageService.add({ severity: 'Error' });
        return of();
      }))
    ))
  );

  downloadBucklingAnalysisResults$ = createEffect(() => this.actions$.pipe(
    ofType(actions.downloadBucklingAnalysisResults),
    withLatestFrom(this.store.select(selectTrackingNumber)),
    map(([, trackingNumber]) => trackingNumber),
    switchMap(trackingNumber => this.httpService.getBucklingAnalysisResults(trackingNumber).pipe(
      this.matchTrackingNumber(trackingNumber),
      map(results => actions.setBucklingAnalysisResults({ results })),
      tap(() => this.messageService.add({ severity: 'success', summary: 'Buckling analysis results ready' })),
      catchError(() => {
        this.messageService.add({ severity: 'Error' });
        return of();
      }))
    ))
  );

  downloadNonLinearAnalysisResults$ = createEffect(() => this.actions$.pipe(
    ofType(actions.downloadNonLinearAnalysisResults),
    withLatestFrom(this.store.select(selectTrackingNumber)),
    map(([, trackingNumber]) => trackingNumber),
    switchMap(trackingNumber => this.httpService.getNonLinearAnalysisResults(trackingNumber).pipe(
      this.matchTrackingNumber(trackingNumber),
      map(results => actions.setNonLinearAnalysisResults({ results })),
      tap(() => this.messageService.add({ severity: 'success', summary: 'Nonlinear analysis results ready' })),
      catchError(() => {
        this.messageService.add({ severity: 'Error' });
        return of();
      }))
    ))
  );



  matchTrackingNumber<T>(trackingNumber: string) {
    return (source$: Observable<T>): Observable<T> => {
      return source$.pipe(
        withLatestFrom(this.store.select(selectTrackingNumber)),
        filter(([, currentTrackingNumber]) => trackingNumber === currentTrackingNumber),
        map(([sourceValue,]) => sourceValue)
      );
    };
  }

}


