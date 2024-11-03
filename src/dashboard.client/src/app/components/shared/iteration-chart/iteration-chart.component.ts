import { Component, Input } from '@angular/core';
import { ChartData, ChartOptions } from 'chart.js';
import { CalculixProgress } from '../../../shared/models/calculix-progress';

const documentStyle = getComputedStyle(document.documentElement);
const textColor = documentStyle.getPropertyValue('--text-color');
const textColorSecondary = documentStyle.getPropertyValue('--text-color-secondary');
const surfaceBorder = documentStyle.getPropertyValue('--surface-border');
const borderColor1 = documentStyle.getPropertyValue('--blue-500');
const borderColor2 = documentStyle.getPropertyValue('--green-500');


@Component({
  selector: 'app-iteration-chart',
  templateUrl: './iteration-chart.component.html',
  styleUrl: './iteration-chart.component.scss'
})
export class IterationChartComponent {
  data: ChartData;
  options: ChartOptions;

  private progressCopy: CalculixProgress[] = [];

  private _progress: CalculixProgress[] = [];
  public get progress(): CalculixProgress[] {
    return this._progress;
  }
  @Input()
  public set progress(value: CalculixProgress[]) {
    this._progress = value;
    this.refreshChart();
    this.progressCopy = value.map(e => ({ ...e }));
  }

  constructor() {
    this.options = this.getOptions();
    this.data = {
      labels: [],
      datasets: [
        {
          label: 'Error',
          fill: false,
          borderColor: borderColor1,
          pointRadius: 0,
          yAxisID: 'y',
          tension: 0,
          data: []
        },
        {
          label: 'Limit',
          fill: false,
          borderColor: borderColor2,
          pointRadius: 0,
          yAxisID: 'y',
          tension: 0,
          data: []
        }
      ]
    };


  }

  private refreshChart() {


    const difference = this._progress.filter((e, i) => i > this.progressCopy.length - 1);
    for (let index = 0; index < difference.length; index++) {
      const item = difference[index];

      this.data.labels?.push(item.iteration);
      this.data.datasets[0].data.push(item.error);
      this.data.datasets[1].data.push(item.limit);

      if (this.data.labels!.length >= 10) {
        this.data.labels!.shift();
        this.data.datasets[0].data.shift();
        this.data.datasets[1].data.shift();
      }

    }

    this.data = { ...this.data };

  }

  private getOptions(): ChartOptions {
    return {
      maintainAspectRatio: false,
      //aspectRatio: 1,
      animation: false,
      plugins: {
        legend: {
          labels: {
            color: textColor
          }
        },
        tooltip: {
          enabled: false
        }
      },
      scales: {
        x: {
          ticks: {
            color: textColorSecondary
          },
          grid: {
            color: surfaceBorder
          }
        },
        y: {
          type: 'linear',
          display: true,
          position: 'left',
          ticks: {
            color: textColorSecondary
          },
          grid: {
            color: surfaceBorder
          },
          title: {
            text: 'Error',
            display: false,
            color: textColorSecondary
          },

        }
      }
    };
  }
}
