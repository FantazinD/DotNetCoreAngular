import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class VehicleService {
  constructor(private http: HttpClient) {}

  getMakes() {
    let arr: any[] = [
      {
        id: 1,
        name: 'Make1',
        models: [
          {
            id: 1,
            name: 'Make1-ModelA',
          },
          {
            id: 2,
            name: 'Make1-ModelB',
          },
          {
            id: 3,
            name: 'Make1-ModelC',
          },
        ],
      },
      {
        id: 2,
        name: 'Make2',
        models: [
          {
            id: 4,
            name: 'Make2-ModelA',
          },
          {
            id: 5,
            name: 'Make2-ModelB',
          },
          {
            id: 6,
            name: 'Make2-ModelC',
          },
        ],
      },
      {
        id: 3,
        name: 'Make3',
        models: [
          {
            id: 7,
            name: 'Make3-ModelA',
          },
          {
            id: 8,
            name: 'Make3-ModelB',
          },
          {
            id: 9,
            name: 'Make3-ModelC',
          },
        ],
      },
    ];
    return arr;
    // return this.http
    //   .get('http://localhost:5166/api/makes')
    //   .pipe(map((res) => res));
  }

  getFeatures() {
    let arr: any[] = [
      {
        id: 1,
        name: 'Feature1',
      },
      {
        id: 2,
        name: 'Feature2',
      },
      {
        id: 3,
        name: 'Feature3',
      },
    ];

    return arr;
    // return this.http
    //   .get('http://localhost:5166/api/features')
    //   .pipe(map((res) => res));
  }
}
