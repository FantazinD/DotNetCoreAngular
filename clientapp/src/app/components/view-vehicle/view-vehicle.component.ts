import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { VehicleService } from '../../../../services/vehicle.service';
import { ToastrService } from 'ngx-toastr';
import { NgbNavModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-view-vehicle',
  standalone: true,
  imports: [RouterModule, NgbNavModule],
  templateUrl: './view-vehicle.component.html',
  styleUrl: './view-vehicle.component.css',
})
export class ViewVehicleComponent implements OnInit {
  active = 1;
  vehicle: any;
  vehicleId: number = 0;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastrService: ToastrService,
    private vehicleService: VehicleService
  ) {
    route.params.subscribe((p) => {
      this.vehicleId = +p['id'];
      if (isNaN(this.vehicleId) || this.vehicleId <= 0) {
        router.navigate(['/vehicles']);
        return;
      }
    });
  }

  ngOnInit(): void {
    this.vehicleService.getVehicle(this.vehicleId).subscribe({
      next: (vehicle) => (this.vehicle = vehicle),
      error: (err) => {
        if (err.status == 404) {
          this.router.navigate(['/vehicles']);
          return;
        }
      },
    });
  }

  onDelete() {
    if (confirm('Are you sure?')) {
      this.vehicleService.deleteVehicle(this.vehicle.id).subscribe((x) => {
        this.router.navigate(['/vehicles']);
      });
    }
  }
}
