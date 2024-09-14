import { Component, OnInit } from '@angular/core';
import { IVehicle } from '../../interfaces/IVehicle';
import { VehicleService } from '../../../../services/vehicle.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-vehicle-list',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './vehicle-list.component.html',
  styleUrl: './vehicle-list.component.css',
})
export class VehicleListComponent implements OnInit {
  vehicles: IVehicle[] = [];

  constructor(private vehicleService: VehicleService) {}

  ngOnInit(): void {
    this.vehicleService
      .getVehicles()
      .subscribe((vehicles: any) => (this.vehicles = vehicles));
  }
}
