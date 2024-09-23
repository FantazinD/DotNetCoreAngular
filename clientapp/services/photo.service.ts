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
    let formData = new FormData();

    formData.append('file', photo);

    return this.http
      .post(`${this.vehiclesEndpoint}/${vehicleId}/photos`, formData)
      .pipe(map((res) => res));
  };

  getPhotos = (vehicleId: number) => {
    return this.http
      .get(`${this.vehiclesEndpoint}/${vehicleId}/photos`)
      .pipe(map((res) => res));
  };
}
