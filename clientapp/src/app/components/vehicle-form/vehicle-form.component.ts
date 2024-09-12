import { FormsModule } from '@angular/forms';
import { VehicleService } from '../../../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin, Observable } from 'rxjs';
import { ISaveVehicle } from '../../interfaces/ISaveVehicle';
import { IVehicle } from '../../interfaces/IVehicle';

@Component({
  selector: 'app-vehicle-form',
  standalone: true,
  imports: [FormsModule, CommonModule],
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

        if (this.vehicle.id) {
          this.setVehicle(data[2]);
          this.populateModels();
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

  onClickDelete = () => {
    if (confirm('Are you sure?')) {
      this.vehicleService.deleteVehicle(this.vehicle.id).subscribe({
        next: (res) => {
          this.router.navigate(['/home']);
        },
      });
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
  };

  onSubmit = (): void => {
    if (this.vehicle.id) {
      this.vehicleService.updateVehicle(this.vehicle).subscribe((res) => {
        this.toastrService.error(
          'Vehicle was sucessfully updated.',
          'Success',
          {
            timeOut: 3000,
            closeButton: true,
          }
        );
      });
    } else {
      this.vehicleService
        .createVehicle(this.vehicle)
        .subscribe((res) => console.log(res));
    }
  };
}
