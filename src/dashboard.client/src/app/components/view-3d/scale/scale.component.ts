import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { setDisplacementScale } from '../../../store/actions';

@Component({
  selector: 'app-scale',
  templateUrl: './scale.component.html',
  styleUrl: './scale.component.scss'
})
export class ScaleComponent {

  constructor(private readonly store: Store) { }


  scaleChanged(increase: number) {
    this.store.dispatch(setDisplacementScale({ increase }))
  }

}
