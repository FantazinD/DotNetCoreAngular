import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

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

  createVehicle = (vehicle: any) => {
    return this.http
      .post('http://localhost:5166/api/vehicles', vehicle)
      .pipe(map((res) => res));
  };
}
