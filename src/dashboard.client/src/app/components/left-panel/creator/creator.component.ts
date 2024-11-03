import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Geometry } from '../../../shared/models/geometry';
import { setGeometry, startCalculations } from '../../../store/actions';
import { Store } from '@ngrx/store';
import { DestroyableComponent } from '../../shared/destroyable-component';
import { filter, first, map, takeUntil, tap } from 'rxjs/operators';
import { selectGeometry } from '../../../store/selectors';

@Component({
  selector: 'app-creator',
  templateUrl: './creator.component.html',
  styleUrl: './creator.component.scss'
})
export class CreatorComponent extends DestroyableComponent implements OnInit {
  geometryForm: FormGroup;

  constructor(private fb: FormBuilder, private readonly store: Store) {
    super();
    this.geometryForm = this.fb.group({
      flangeThickness: [0, [Validators.required, Validators.min(0)]],
      webThickness: [0, [Validators.required, Validators.min(0)]],
      height: [0, [Validators.required, Validators.min(0)]],
      width: [0, [Validators.required, Validators.min(0)]],
      length: [0, [Validators.required, Validators.min(0)]],
      uniformLoad: [0, [Validators.required]],
      imperfection: [0, [Validators.required, Validators.min(0)]]
    });
  }

  ngOnInit(): void {

    this.geometryForm.valueChanges.pipe(
      takeUntil(this.destroyed$),
      filter(() => this.geometryForm.valid),
      map(e => <Geometry>e),
      tap(geometry => this.store.dispatch(setGeometry({ geometry })))
    ).subscribe();

    this.store.select(selectGeometry).pipe(
      first(),
      tap(e => this.geometryForm.setValue(e))
    ).subscribe();

  }

  onSubmit() {
    if (this.geometryForm.valid) {
      this.store.dispatch(startCalculations());
    } else {
      console.log('Form is invalid');
    }
  }
}
