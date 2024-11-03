import { Component, OnInit } from '@angular/core';
import { DestroyableComponent } from '../../shared/destroyable-component';
import { Store } from '@ngrx/store';
import { analysisTypeChanged, setMeshVisible, stressTypeChanged } from '../../../store/actions';
import { stressTypes } from '../../../shared/types/stress-type';
import { AnalysisType } from '../../../shared/types/analysis-type';
import { selectAnalysisType, selectBucklingAnalysisResults, selectLinearAnalysisResults, selectNonLinearAnalysisResults } from '../../../store/selectors';
import { takeUntil, tap } from 'rxjs';

@Component({
  selector: 'app-results',
  templateUrl: './results.component.html',
  styleUrl: './results.component.scss'
})
export class ResultsComponent extends DestroyableComponent implements OnInit {
  meshModel: boolean = true;

  stressTypes = [...stressTypes];
  selectedStressType = this.stressTypes[0];

  selectedAnalysisType: AnalysisType = 'linear';

  linearDisabled = true;
  bucklingDisabled = true;
  nonLinearDisabled = true;
  stressDisabled = true;

  constructor(private readonly store: Store) { super(); }

  ngOnInit(): void {
    this.store.select(selectLinearAnalysisResults).pipe(
      takeUntil(this.destroyed$),
      tap(r => this.linearDisabled = r === null)
    ).subscribe();

    this.store.select(selectBucklingAnalysisResults).pipe(
      takeUntil(this.destroyed$),
      tap(r => this.bucklingDisabled = r === null)
    ).subscribe();

    this.store.select(selectNonLinearAnalysisResults).pipe(
      takeUntil(this.destroyed$),
      tap(r => this.nonLinearDisabled = r === null)
    ).subscribe();

    this.store.select(selectAnalysisType).pipe(
      takeUntil(this.destroyed$),
      tap(a => this.stressDisabled = a === 'buckling')
    ).subscribe();
  }

  stressTypeChanged(): void {
    this.store.dispatch(stressTypeChanged({ stressType: this.selectedStressType }));
  }

  analysisTypeChanged(): void {
    this.store.dispatch(analysisTypeChanged({ analysisType: this.selectedAnalysisType }));
  }

  meshChecked(): void {
    this.store.dispatch(setMeshVisible({ visible: this.meshModel }));
  }
}
