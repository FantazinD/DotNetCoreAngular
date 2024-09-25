import { PhotoService } from './../../../../services/photo.service';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { VehicleService } from '../../../../services/vehicle.service';
import { ToastrService } from 'ngx-toastr';
import { NgbNavModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpEventType } from '@angular/common/http';

@Component({
  selector: 'app-view-vehicle',
  standalone: true,
  imports: [RouterModule, NgbNavModule],
  templateUrl: './view-vehicle.component.html',
  styleUrl: './view-vehicle.component.css',
})
export class ViewVehicleComponent implements OnInit {
  @ViewChild('fileInput', { read: ElementRef }) fileInput!: ElementRef;
  active = 1;
  vehicle: any;
  vehicleId: number = 0;
  photos: any[] = [];
  progress: any = {
    percentage: 10,
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastrService: ToastrService,
    private vehicleService: VehicleService,
    private photoService: PhotoService
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
    this.photoService.getPhotos(this.vehicleId).subscribe({
      next: (photos: any) => (this.photos = photos),
      error: (err) => {},
    });

    this.vehicleService.getVehicle(this.vehicleId).subscribe({
      next: (vehicle) => {
        this.vehicle = vehicle;
      },
      error: (err) => {
        if (err.status == 404) {
          this.router.navigate(['/vehicles']);
          return;
        }
      },
    });
  }

  onDelete = () => {
    if (confirm('Are you sure?')) {
      this.vehicleService.deleteVehicle(this.vehicle.id).subscribe((x) => {
        this.router.navigate(['/vehicles']);
      });
    }
  };

  onUploadPhoto = () => {
    var nativeElement: HTMLInputElement = this.fileInput.nativeElement;

    this.photoService
      .uploadPhoto(this.vehicleId, nativeElement.files![0])
      .subscribe({
        next: (event: any) => {
          if (event.type === HttpEventType.UploadProgress) {
            this.progress.percentage = Math.round(
              (100 * event.loaded) / event.total
            );
          } else if (event.type === HttpEventType.Response) {
            // Complete
            this.toastrService.success(
              `Photo sucessfully uploaded.`,
              'Success',
              {
                timeOut: 3000,
                closeButton: true,
              }
            );
            this.progress.percentage = 100;
            this.photos.push(event.body);
          }
        },
        error: (err) => {
          this.toastrService.error(err.statusText, 'Error', {
            timeOut: 3000,
            closeButton: true,
          });
        },
        complete: () => {
          this.progress = null;
        },
      });
  };
}
