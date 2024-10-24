import { Component, OnInit } from '@angular/core';
import { VehicleService } from '../../../../services/vehicle.service';
import { RouterModule } from '@angular/router';
import { IKeyValuePair } from '../../interfaces/IKeyValuePair';
import { FormsModule } from '@angular/forms';
import { PaginationComponent } from '../shared/pagination/pagination.component';
import { AuthService } from '@auth0/auth0-angular';
import { CommonModule } from '@angular/common';
import { ConfigService } from '../../../../services/config.service';

@Component({
  selector: 'app-vehicle-list',
  standalone: true,
  imports: [RouterModule, FormsModule, PaginationComponent, CommonModule],
  templateUrl: './vehicle-list.component.html',
  styleUrl: './vehicle-list.component.css',
})
export class VehicleListComponent implements OnInit {
  queryResult: any = {};
  makes: IKeyValuePair[] = [];
  query: any = {
    pageSize: 10,
  };
  columns: any[] = [
    { title: 'Id' },
    { title: 'Make', key: 'make', isSortable: true },
    {
      title: 'Model',
      key: 'model',
      isSortable: true,
    },
    {
      title: 'Contact Name',
      key: 'contactName',
      isSortable: true,
    },
    { title: '', key: 'action' },
  ];

  constructor(
    private vehicleService: VehicleService,
    public auth: AuthService,
    private configService: ConfigService
  ) {
    this.query.pageSize = configService.pageSize;
  }

  ngOnInit(): void {
    this.vehicleService
      .getMakes()
      .subscribe((makes: any) => (this.makes = makes));

    this.populateVehicles();
  }

  private populateVehicles = () => {
    this.vehicleService
      .getVehicles(this.query)
      .subscribe((res: any) => (this.queryResult = res));
  };

  onFilterChange = () => {
    this.query.page = 1;
    this.populateVehicles();
  };

  onResetFilter = () => {
    this.query = {
      page: 1,
      pageSize: this.configService.pageSize,
    };
    this.populateVehicles();
  };

  sortBy = (columnName: string) => {
    if (this.query.sortBy === columnName)
      this.query.isSortAscending = !this.query.isSortAscending;
    else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populateVehicles();
  };

  onPageChange = (page: number) => {
    this.query.page = page;
    this.populateVehicles();
  };
}
