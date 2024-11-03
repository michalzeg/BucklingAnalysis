import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


import { ButtonModule } from 'primeng/button';
import { ButtonGroupModule } from 'primeng/buttongroup';
import { CardModule } from 'primeng/card';
import { PanelModule } from 'primeng/panel';
import { TabViewModule } from 'primeng/tabview';
import { ChartModule } from 'primeng/chart';
import { TableModule } from 'primeng/table';
import { InputGroupModule } from 'primeng/inputgroup';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { DropdownModule } from 'primeng/dropdown';
import { SliderModule } from 'primeng/slider';
import { InputTextModule } from 'primeng/inputtext';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { TagModule } from 'primeng/tag';
import { TabMenuModule } from 'primeng/tabmenu';
import { ToastModule } from 'primeng/toast';
import { InputNumberModule } from 'primeng/inputnumber';
import { MessageService } from 'primeng/api';
import { InputSwitchModule } from 'primeng/inputswitch';
import { TimelineModule } from 'primeng/timeline';
import { ChipModule } from 'primeng/chip';
import { RadioButtonModule } from 'primeng/radiobutton';

import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { appReducer } from './store/reducer';
import { AppEffects } from './store/effects/app-effects';
import { LeftPanelComponent } from './components/left-panel/left-panel.component';
import { LegendComponent } from './components/view-3d/legend/legend.component';
import { View3dComponent } from './components/view-3d/view3d.component';
import { SignalREffects } from './store/effects/signalR-effects';
import { ProgressComponent } from './components/left-panel/progress/progress.component';
import { IterationChartComponent } from './components/shared/iteration-chart/iteration-chart.component';
import { ResultsComponent } from './components/left-panel/results/results.component';
import { ScaleComponent } from './components/view-3d/scale/scale.component';
import { CreatorComponent } from './components/left-panel/creator/creator.component';
import { SummaryComponent } from './components/left-panel/results/summary/summary.component';
import { DrawingComponent } from './components/left-panel/creator/drawing/drawing.component';
import { Drawing2dDirective } from './components/shared/drawing/drawing2d.directive';
import { HttpEffects } from './store/effects/http-effects';


@NgModule({
  declarations: [
    AppComponent,
    View3dComponent,
    LegendComponent,
    LeftPanelComponent,
    ProgressComponent,
    IterationChartComponent,
    ResultsComponent,
    ScaleComponent,
    CreatorComponent,
    SummaryComponent,
    DrawingComponent,
    Drawing2dDirective
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    StoreModule.forRoot({ app: appReducer }),
    EffectsModule.forRoot([AppEffects, SignalREffects, HttpEffects]),
    // StoreDevtoolsModule.instrument({
    //   maxAge: 25,
    //   logOnly: environment.storeEnabled
    // }),
    ButtonModule,
    ButtonGroupModule,
    CardModule,
    PanelModule,
    TabViewModule,
    ChartModule,
    TableModule,
    InputGroupModule,
    InputGroupAddonModule,
    DropdownModule,
    SliderModule,
    TabMenuModule,
    InputTextModule,
    ProgressSpinnerModule,
    TagModule,
    ToastModule,
    InputNumberModule,
    InputSwitchModule,
    TimelineModule,
    ChipModule,
    RadioButtonModule
  ],
  providers: [MessageService],
  bootstrap: [AppComponent]
})
export class AppModule { }
