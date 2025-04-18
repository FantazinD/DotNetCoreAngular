import { LoadingComponent } from '../shared/loading/loading.component';
import { VehicleService } from '../../../../services/vehicle.service';
import { ISaveVehicle } from '../../interfaces/ISaveVehicle';
import { ActivatedRoute, Router } from '@angular/router';
import { IVehicle } from '../../interfaces/IVehicle';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-vehicle-form',
  standalone: true,
  imports: [FormsModule, CommonModule, LoadingComponent],
  templateUrl: './vehicle-form.component.html',
  styleUrl: './vehicle-form.component.css',
})
export class VehicleFormComponent implements OnInit {
  vehicle: ISaveVehicle = {
    id: 0,
    makeId: 0,
    modelId: 0,
    isRegistered: false,
    features: [],
    contact: {
      name: '',
      phone: '',
      email: '',
    },
  };
  isLoading: boolean = false;
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
      this.vehicle.id = +p['id'] || 0;
    });
  }

  ngOnInit(): void {
    this.isLoading = true;
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

        if (this.vehicle.id) {
          this.setVehicle(data[2]);
          this.populateModels();
        } else {
          this.isLoading = false;
        }
      },
      error: (err) => {
        if (err.status == 404) this.router.navigate(['/home']);
      },
    });
  }

  private setVehicle = (vehicle: IVehicle) => {
    this.vehicle.id = vehicle.id;
    this.vehicle.makeId = vehicle.make.id;
    this.vehicle.modelId = vehicle.model.id;
    this.vehicle.isRegistered = vehicle.isRegistered;
    this.vehicle.contact = vehicle.contact;
    this.vehicle.features = vehicle.features.map((feature) => feature.id);
  };

  onFeatureToggle = (featureId: any, $event: any): void => {
    if ($event.target.checked) this.vehicle.features.push(featureId);
    else {
      let index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index, 1);
    }
  };

  onClickBack = () => {
    this.router.navigate(['/vehicles']);
  };

  onClickDelete = () => {
    if (confirm('Are you sure?')) {
      this.isLoading = true;
      this.vehicleService.deleteVehicle(this.vehicle.id).subscribe({
        next: (res) => {
          this.toastrService.success(
            'Vehicle was sucessfully deleted.',
            'Success',
            {
              timeOut: 3000,
              closeButton: true,
            }
          );
          this.router.navigate(['/vehicles']);
        },
        error: (err) => {
          this.toastrService.error(
            'Vehicle may have been already deleted.',
            'Unexpected Error',
            {
              timeOut: 3000,
              closeButton: true,
            }
          );
          this.router.navigate(['/vehicles']);
        },
      });
      this.isLoading = false;
    }
  };

  onMakeChange = (): void => {
    this.populateModels();
    delete this.vehicle.modelId;
  };

  private populateModels = () => {
    let selectedMake = this.makes.find(
      (make) => make.id == this.vehicle.makeId
    );
    this.models = selectedMake ? selectedMake.models : [];
    this.isLoading = false;
  };

  onSubmit = (): void => {
    this.isLoading = true;
    var result$ = this.vehicle.id
      ? this.vehicleService.updateVehicle(this.vehicle)
      : this.vehicleService.createVehicle(this.vehicle);
    result$.subscribe({
      next: () => {
        this.toastrService.success(
          `Vehicle was sucessfully ${this.vehicle.id ? 'updated' : 'created'}.`,
          'Success',
          {
            timeOut: 3000,
            closeButton: true,
          }
        );
        this.router.navigate(['/vehicles']);
      },
      error: (err) => {
        this.toastrService.error(
          `If the issue persists, contact support.`,
          'Unexpected Error.',
          {
            timeOut: 3000,
            closeButton: true,
          }
        );
        this.router.navigate(['/vehicles']);
      },
    });
    this.isLoading = false;
  };
}
