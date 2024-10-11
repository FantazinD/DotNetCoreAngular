import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { ISaveVehicle } from '../src/app/interfaces/ISaveVehicle';

@Injectable({
  providedIn: 'root',
})
export class VehicleService {
  private readonly apiEndpoint = `http://localhost:5166/api`;
  private readonly headers = new HttpHeaders({
    Authorization: `Bearer ${localStorage.getItem('token')}`,
  });

  constructor(private http: HttpClient) {}

  getMakes = () => {
    return this.http.get(`${this.apiEndpoint}/makes`).pipe(map((res) => res));
  };

  getFeatures = () => {
    return this.http
      .get(`${this.apiEndpoint}/features`)
      .pipe(map((res) => res));
  };

  createVehicle = (vehicle: ISaveVehicle) => {
    return this.http
      .post(`${this.apiEndpoint}/vehicles`, vehicle, {
        headers: this.headers,
      })
      .pipe(map((res) => res));
  };

  getVehicle = (id: number) => {
    return this.http
      .get(`${this.apiEndpoint}/vehicles/${id}`)
      .pipe(map((res) => res));
  };

  getVehicles = (filter: any) => {
    return this.http
      .get(`${this.apiEndpoint}/vehicles?${this.toQueryString(filter)}`)
      .pipe(map((res) => res));
  };

  toQueryString = (filterObj: any) => {
    let queryParts = [];

    for (let property in filterObj) {
      let value = filterObj[property];
      if (value != null && value != undefined)
        queryParts.push(`${encodeURIComponent(property)}=${value}`);
    }

    return queryParts.join('&');
  };

  updateVehicle = (vehicle: ISaveVehicle) => {
    return this.http
      .put(`${this.apiEndpoint}/vehicles/${vehicle.id}`, vehicle, {
        headers: this.headers,
      })
      .pipe(map((res) => res));
  };

  deleteVehicle = (id: number) => {
    return this.http
      .delete(`${this.apiEndpoint}/vehicles/${id}`, {
        headers: this.headers,
      })
      .pipe(map((res) => res));
  };
}
