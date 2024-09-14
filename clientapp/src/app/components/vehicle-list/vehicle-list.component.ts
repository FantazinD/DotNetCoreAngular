import { Component, OnInit } from '@angular/core';
import { IVehicle } from '../../interfaces/IVehicle';
import { VehicleService } from '../../../../services/vehicle.service';
import { RouterModule } from '@angular/router';
import { IKeyValuePair } from '../../interfaces/IKeyValuePair';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-vehicle-list',
  standalone: true,
  imports: [RouterModule, FormsModule],
  templateUrl: './vehicle-list.component.html',
  styleUrl: './vehicle-list.component.css',
})
export class VehicleListComponent implements OnInit {
  vehicles: IVehicle[] = [];
  makes: IKeyValuePair[] = [];
  filter: any = {};

  constructor(private vehicleService: VehicleService) {}

  ngOnInit(): void {
    this.vehicleService
      .getMakes()
      .subscribe((makes: any) => (this.makes = makes));

    this.populateVehicles();
  }

  private populateVehicles = () => {
    this.vehicleService
      .getVehicles(this.filter)
      .subscribe((vehicles: any) => (this.vehicles = vehicles));
  };

  onFilterChange = () => {
    this.populateVehicles();
  };

  onResetFilter = () => {
    this.filter = {};
    this.onFilterChange();
  };
}
