import { PhotoService } from './../../../../services/photo.service';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
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
  @ViewChild('fileInput', { read: ElementRef }) fileInput!: ElementRef;
  active = 1;
  vehicle: any;
  vehicleId: number = 0;
  photos: any[] = [];

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
    console.log('onInit');
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
    console.log(this.fileInput);
    var nativeElement: HTMLInputElement = this.fileInput.nativeElement;

    this.photoService
      .uploadPhoto(this.vehicleId, nativeElement.files![0])
      .subscribe({
        next: (photo) => this.photos.push(photo),
        error: (err) => {
          console.log(err);
        },
      });
  };
}
