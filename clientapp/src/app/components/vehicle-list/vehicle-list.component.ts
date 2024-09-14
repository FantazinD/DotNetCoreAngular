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
  allVehicles: IVehicle[] = [];
  makes: IKeyValuePair[] = [];
  filter: any = {};

  constructor(private vehicleService: VehicleService) {}

  ngOnInit(): void {
    this.vehicleService
      .getMakes()
      .subscribe((makes: any) => (this.makes = makes));

    this.vehicleService
      .getVehicles()
      .subscribe(
        (vehicles: any) => (this.vehicles = this.allVehicles = vehicles)
      );
  }

  onFilterChange = () => {
    var vehicles = this.allVehicles;

    if (this.filter.makeId)
      vehicles = vehicles.filter(
        (vehicles) => vehicles.make.id == this.filter.makeId
      );

    this.vehicles = vehicles;
  };

  onResetFilter = () => {
    this.filter = {};
    this.onFilterChange();
  };
}
