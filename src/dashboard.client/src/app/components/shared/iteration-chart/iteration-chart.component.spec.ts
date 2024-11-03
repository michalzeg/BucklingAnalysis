import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IterationChartComponent } from './iteration-chart.component';

describe('IterationChartComponent', () => {
  let component: IterationChartComponent;
  let fixture: ComponentFixture<IterationChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [IterationChartComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(IterationChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
