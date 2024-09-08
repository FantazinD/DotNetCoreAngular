import { FormsModule } from '@angular/forms';
import { VehicleService } from '../../../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-vehicle-form',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './vehicle-form.component.html',
  styleUrl: './vehicle-form.component.css',
})
export class VehicleFormComponent implements OnInit {
  vehicle: any = {};

  makes: any[] = [];
  models: any[] = [];
  features: any[] = [];

  constructor(private vehicleService: VehicleService) {}

  ngOnInit(): void {
    this.vehicleService
      .getMakes()
      .subscribe((makes: any) => (this.makes = makes));
    this.vehicleService
      .getFeatures()
      .subscribe((features: any) => (this.features = features));
    //this.makes = this.vehicleService.getMakes();
    //this.features = this.vehicleService.getFeatures();
  }

  onMakeChange = (): void => {
    let selectedMake = this.makes.find(
      (make) => make.id == this.vehicle.makeId
    );
    this.models = selectedMake ? selectedMake.models : [];
    delete this.vehicle.modelId;
  };
}
