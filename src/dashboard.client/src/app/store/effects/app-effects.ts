import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { filter, map } from 'rxjs/operators';
import * as actions from '../actions';
import { Store } from '@ngrx/store';
import { AppState } from '../state';
import { Router } from '@angular/router';
@Injectable()
export class AppEffects {

  constructor(
    private actions$: Actions,
    private readonly store: Store<AppState>,
    private readonly router: Router
  ) { }

  loadTrackingNumber$ = createEffect(() => this.actions$.pipe(
    ofType(actions.loadTrackingNumber),
    map(() => localStorage.getItem('trackingNumber')),
    filter(t => t !== null && t.trim() !== ''),
    map(t => actions.setTrackingNumber({ trackingNumber: t! }))
  ));



}


