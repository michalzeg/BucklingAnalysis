import { Component, OnInit } from '@angular/core';
import { DestroyableComponent } from '../../shared/destroyable-component';
import { Store } from '@ngrx/store';
import { ActivityProgress } from '../../../shared/models/activity-progress';
import { takeUntil, tap } from 'rxjs';
import { selectActivityProgress, selectCalculixLinearAnalysisProgress } from '../../../store/selectors';
import { CalculixProgress } from '../../../shared/models/calculix-progress';

@Component({
  selector: 'app-progress',
  templateUrl: './progress.component.html',
  styleUrl: './progress.component.scss'
})
export class ProgressComponent extends DestroyableComponent implements OnInit {
  activityProgress: ActivityProgress[] = [];
  linearAnalysisCalculixProgress: CalculixProgress[] = [];

  constructor(private readonly store: Store) { super(); }

  ngOnInit(): void {
    this.store.select(selectActivityProgress).pipe(
      takeUntil(this.destroyed$),
      tap(e => this.activityProgress = e)
    ).subscribe();

    this.store.select(selectCalculixLinearAnalysisProgress).pipe(
      takeUntil(this.destroyed$),
      tap(e => this.linearAnalysisCalculixProgress = e)
    ).subscribe();
  }
}
