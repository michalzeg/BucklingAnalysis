import { Component } from '@angular/core';
import { DestroyableComponent } from '../shared/destroyable-component';
import { Store } from '@ngrx/store';

@Component({
  selector: 'app-left-panel',
  templateUrl: './left-panel.component.html',
  styleUrl: './left-panel.component.scss'
})
export class LeftPanelComponent extends DestroyableComponent {

  activeIndex: number = 0;

  constructor(private readonly store: Store) { super(); }



}
