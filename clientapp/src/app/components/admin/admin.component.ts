import { BaseChartDirective } from 'ng2-charts';
import { Component } from '@angular/core';
import { ChartOptions } from 'chart.js';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [BaseChartDirective],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css',
})
export class AdminComponent {
  // Pie
  public pieChartOptions: ChartOptions<'pie'> = {
    responsive: false,
  };
  public pieChartLabels = [
    ['Download', 'Sales'],
    ['In', 'Store', 'Sales'],
    'Mail Sales',
  ];
  public pieChartDatasets = [
    {
      data: [300, 500, 100],
    },
  ];
  public pieChartLegend = true;
  public pieChartPlugins = [];
}
