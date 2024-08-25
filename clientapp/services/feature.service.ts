import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class FeatureService {
  constructor(private http: HttpClient) {}

  getFeatures() {
    // return this.http
    //   .get('http://localhost:5166/api/features')
    //   .pipe(map((res) => res));
  }
}
