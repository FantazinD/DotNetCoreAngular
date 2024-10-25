import { PhotoService } from './../../../../services/photo.service';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { VehicleService } from '../../../../services/vehicle.service';
import { ToastrService } from 'ngx-toastr';
import {
  NgbCarouselConfig,
  NgbCarouselModule,
  NgbNavModule,
} from '@ng-bootstrap/ng-bootstrap';
import { HttpEventType } from '@angular/common/http';
import { AuthService } from '@auth0/auth0-angular';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-view-vehicle',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './view-vehicle.component.html',
  styleUrl: './view-vehicle.component.css',
  providers: [],
})
export class ViewVehicleComponent implements OnInit {
  @ViewChild('fileInput', { read: ElementRef }) fileInput!: ElementRef;
  activeTab: string = 'tabs-vehicle';
  activeCarouselSlide: number = 0;
  vehicle: any;
  vehicleId: number = 0;
  photos: any[] = [];
  progress: any = {};

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastrService: ToastrService,
    private vehicleService: VehicleService,
    private photoService: PhotoService,
    public auth: AuthService
  ) {
    this.route.params.subscribe((p) => {
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

  setActiveTab = (tabId: string) => {
    this.activeTab = tabId;
  };

  setActiveSlide = (index: number) => {
    if (index < 0) {
      this.activeCarouselSlide = this.photos.length - 1;
    } else if (index >= this.photos.length) {
      this.activeCarouselSlide = 0;
    } else {
      this.activeCarouselSlide = index;
    }
  };

  onDelete = () => {
    if (confirm('Are you sure?')) {
      this.vehicleService.deleteVehicle(this.vehicle.id).subscribe((x) => {
        this.router.navigate(['/vehicles']);
      });
    }
  };

  onUploadPhoto = () => {
    const nativeElement: HTMLInputElement = this.fileInput.nativeElement;
    const file = nativeElement.files![0];
    nativeElement.value = '';

    this.photoService.uploadPhoto(this.vehicleId, file).subscribe({
      next: (event: any) => {
        if (event.type === HttpEventType.UploadProgress) {
          this.progress.percentage = Math.round(
            (100 * event.loaded) / event.total
          );
        } else if (event.type === HttpEventType.Response) {
          // Complete
          this.toastrService.success(`Photo sucessfully uploaded.`, 'Success', {
            timeOut: 3000,
            closeButton: true,
          });
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
