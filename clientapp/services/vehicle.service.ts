import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ISaveVehicle } from '../src/app/interfaces/ISaveVehicle';

@Injectable({
  providedIn: 'root',
})
export class VehicleService {
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
      .post('http://localhost:5166/api/vehicles', vehicle)
      .pipe(map((res) => res));
  };

  getVehicle = (id: number) => {
    return this.http
      .get(`http://localhost:5166/api/vehicles/${id}`)
      .pipe(map((res) => res));
  };

  updateVehicle = (vehicle: ISaveVehicle) => {
    return this.http
      .put(`http://localhost:5166/api/vehicles/${vehicle.id}`, vehicle)
      .pipe(map((res) => res));
  };

  deleteVehicle = (id: number) => {
    return this.http
      .delete(`http://localhost:5166/api/vehicles/${id}`)
      .pipe(map((res) => res));
  };
}
