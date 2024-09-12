import { FormsModule } from '@angular/forms';
import { VehicleService } from '../../../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { Router } from 'express';

@Component({
  selector: 'app-vehicle-form',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './vehicle-form.component.html',
  styleUrl: './vehicle-form.component.css',
})
export class VehicleFormComponent implements OnInit {
  vehicle: any = {
    features: [],
    contact: {},
  };

  makes: any[] = [];
  models: any[] = [];
  features: any[] = [];

  constructor(
    private route: ActivatedRoute,
    // private router: Router,
    private vehicleService: VehicleService,
    private toastrService: ToastrService
  ) {
    this.route.params.subscribe({
      next: (p) => {
        this.vehicle.id = +p['id'];
      },
    });
  }

  ngOnInit(): void {
    this.vehicleService
      .getVehicle(this.vehicle.id)
      .subscribe((vehicle: any) => (this.vehicle = vehicle));
    this.vehicleService
      .getMakes()
      .subscribe((makes: any) => (this.makes = makes));
    this.vehicleService
      .getFeatures()
      .subscribe((features: any) => (this.features = features));
  }

  showSuccess() {
    this.toastrService.success('Hello world!', 'Toastr fun!');
  }

  onFeatureToggle = (featureId: any, $event: any): void => {
    if ($event.target.checked) this.vehicle.features.push(featureId);
    else {
      let index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index, 1);
    }
  };

  onMakeChange = (): void => {
    let selectedMake = this.makes.find(
      (make) => make.id == this.vehicle.makeId
    );
    this.models = selectedMake ? selectedMake.models : [];
    delete this.vehicle.modelId;
  };

  onSubmit = (): void => {
    this.vehicleService.createVehicle(this.vehicle).subscribe({
      next: (x) => {
        this.showSuccess();
      },
    });
  };
}
