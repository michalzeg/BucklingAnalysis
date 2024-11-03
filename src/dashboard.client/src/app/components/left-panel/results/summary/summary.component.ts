import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { combineLatest, takeUntil, withLatestFrom, map, filter, tap } from 'rxjs';
import { selectStressType, selectLinearAnalysisResults, selectNonLinearAnalysisResults } from '../../../../store/selectors';
import { DestroyableComponent } from '../../../shared/destroyable-component';
import { ExtremeValues } from '../../../../shared/models/extreme-values';
import { getExtremeValues } from '../../../../shared/utils/get-extreme-values';




@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html',
  styleUrl: './summary.component.scss'
})
export class SummaryComponent extends DestroyableComponent implements OnInit {
  extremes: ExtremeValues[] = [];
  visible = false;
  constructor(private readonly store: Store) {
    super();
  }
  ngOnInit(): void {
    combineLatest([
      this.store.select(selectLinearAnalysisResults),
      this.store.select(selectNonLinearAnalysisResults)
    ]).pipe(
      takeUntil(this.destroyed$),
      tap(([linear, nonLinear]) => this.visible = linear !== null && nonLinear !== null)

    ).subscribe();


    combineLatest([
      this.store.select(selectStressType),
      this.store.select(selectLinearAnalysisResults),
      this.store.select(selectNonLinearAnalysisResults)
    ]).pipe(
      takeUntil(this.destroyed$),
      withLatestFrom(
        this.store.select(selectLinearAnalysisResults),
        this.store.select(selectNonLinearAnalysisResults)
      ),
      map(([[stressType], linearResults, nonLinearResults]) => ({ stressType, linearResults, nonLinearResults })),
      filter(e => e.linearResults !== null && e.nonLinearResults !== null),
      map(e => {
        const linearExtremes = getExtremeValues(e.stressType, 'linear', e.linearResults!);
        const nonLinearExtremes = getExtremeValues(e.stressType, 'non-linear', e.nonLinearResults!);
        return [
          linearExtremes, nonLinearExtremes
        ];

      }),
      tap(e => this.extremes = e)
    ).subscribe();

  }
}
