import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class FeatureService {
  constructor(private http: HttpClient) {}

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
