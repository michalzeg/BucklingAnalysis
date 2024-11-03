import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { DestroyableComponent } from '../../shared/destroyable-component';
import { ColorService } from '../../../services/color.service';
import { combineLatest, filter, map, takeUntil, tap } from 'rxjs';
import { selectAnalysisType, selectBucklingAnalysisResults, selectLinearAnalysisResults, selectNonLinearAnalysisResults, selectStressType } from '../../../store/selectors';
import { getExtremeValues } from '../../../shared/utils/get-extreme-values';

@Component({
  selector: 'app-legend',
  templateUrl: './legend.component.html',
  styleUrl: './legend.component.scss',
  //changeDetection: ChangeDetectionStrategy.OnPush
})
export class LegendComponent extends DestroyableComponent implements OnInit {

  gradient: string = ``;

  min = 0;
  middle = 0;
  max = 0;

  visible = false;

  constructor(private readonly store: Store, private readonly colorService: ColorService, private readonly detector: ChangeDetectorRef) { super(); }

  ngOnInit(): void {

    combineLatest([
      this.store.select(selectStressType),
      this.store.select(selectAnalysisType),
      this.store.select(selectLinearAnalysisResults),
      this.store.select(selectNonLinearAnalysisResults),
      this.store.select(selectBucklingAnalysisResults)
    ]).pipe(
      takeUntil(this.destroyed$),
      tap(([, analysisType,]) => this.visible = analysisType !== 'buckling'),
      filter(([, analysisType,]) => analysisType !== 'buckling'),

      map(([stressType, analysisType, linearResults, nonLinearResults,]) => ({ stressType, analysisType, results: analysisType === 'linear' ? linearResults : nonLinearResults })),
      tap(e => this.visible = e.results !== null),
      filter(e => e.results !== null),
      map(e => getExtremeValues(e.stressType, e.analysisType, e.results!)),
      tap(e => this.max = e.max),
      tap(e => this.min = e.min),
      tap(e => this.middle = (e.max - e.min) / 2),
      map(() => this.colorService.getColorRange()),
      tap(r => this.buildGradient(r)),
      //tap(() => this.detector.detectChanges())
    ).subscribe();

    //this.detector.detectChanges();
  }

  private buildGradient(colors: Array<string>): void {
    const colorsString = colors.reverse().join(', ');

    this.gradient = `linear-gradient(to bottom, ${colorsString})`;
  }


}
