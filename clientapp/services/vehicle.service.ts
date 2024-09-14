import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ISaveVehicle } from '../src/app/interfaces/ISaveVehicle';

@Injectable({
  providedIn: 'root',
})
export class VehicleService {
  private readonly vehiclesEndpoint = `http://localhost:5166/api/vehicles`;

  constructor(private http: HttpClient) {}

  getMakes = () => {
    return this.http
      .get('http://localhost:5166/api/makes')
      .pipe(map((res) => res));
  };

  getFeatures = () => {
    return this.http
      .get('http://localhost:5166/api/features')
      .pipe(map((res) => res));
  };

  createVehicle = (vehicle: ISaveVehicle) => {
    return this.http
      .post(this.vehiclesEndpoint, vehicle)
      .pipe(map((res) => res));
  };

  getVehicle = (id: number) => {
    return this.http
      .get(`${this.vehiclesEndpoint}/${id}`)
      .pipe(map((res) => res));
  };

  getVehicles() {
    return this.http.get(this.vehiclesEndpoint).pipe(map((res) => res));
  }

  updateVehicle = (vehicle: ISaveVehicle) => {
    return this.http
      .put(`${this.vehiclesEndpoint}/${vehicle.id}`, vehicle)
      .pipe(map((res) => res));
  };

  deleteVehicle = (id: number) => {
    return this.http
      .delete(`${this.vehiclesEndpoint}/${id}`)
      .pipe(map((res) => res));
  };
}
