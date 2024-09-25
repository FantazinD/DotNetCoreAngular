import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PhotoService {
  private readonly vehiclesEndpoint = `http://localhost:5166/api/vehicles`;

  constructor(private http: HttpClient) {}

  uploadPhoto = (vehicleId: number, photo: any) => {
    const formData: FormData = new FormData();

    formData.append('file', photo);

    return this.http.post(
      `${this.vehiclesEndpoint}/${vehicleId}/photos`,
      formData,
      {
        reportProgress: true,
        observe: 'events',
      }
    );
  };

  getPhotos = (vehicleId: number) => {
    return this.http
      .get(`${this.vehiclesEndpoint}/${vehicleId}/photos`)
      .pipe(map((res) => res));
  };
}
