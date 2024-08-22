import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MakeService {
  constructor(private http: HttpClient) {}

  getMakes() {
    return this.http.get('/api/makes').pipe(map((res: any) => res.json()));
  }
}
