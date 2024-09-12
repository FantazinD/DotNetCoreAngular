import { FormsModule } from '@angular/forms';
import { VehicleService } from '../../../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin, Observable } from 'rxjs';

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
    private router: Router,
    private vehicleService: VehicleService,
    private toastrService: ToastrService
  ) {
    this.route.params.subscribe((p) => {
      this.vehicle.id = +p['id'];
    });
  }

  ngOnInit(): void {
    let sources = [
      this.vehicleService.getMakes(),
      this.vehicleService.getFeatures(),
    ];

    if (this.vehicle.id)
      sources.push(this.vehicleService.getVehicle(this.vehicle.id));

    forkJoin(sources).subscribe({
      next: (data: any[]) => {
        this.makes = data[0];
        this.features = data[1];

        if (this.vehicle.id) this.setVehicle(data[2]);
      },
      error: (err) => {
        if (err.status == 404) this.router.navigate(['/home']);
      },
    });
  }

  private setVehicle = (vehicle: any) => {
    this.vehicle.id = vehicle.id;
    this.vehicle.makeId = vehicle.make.id;
    this.vehicle.modelId = vehicle.model.id;
  };

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
