import { ConfigService } from './config.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PhotoService {
  constructor(private http: HttpClient, private configService: ConfigService) {}

  uploadPhoto = (vehicleId: number, photo: any) => {
    const formData: FormData = new FormData();

    formData.append('file', photo);

    return this.http.post(
      `${this.configService.apiUrl}/vehicles/${vehicleId}/photos`,
      formData,
      {
        reportProgress: true,
        observe: 'events',
      }
    );
  };

  getPhotos = (vehicleId: number) => {
    return this.http
      .get(`${this.configService.apiUrl}/vehicles/${vehicleId}/photos`)
      .pipe(map((res) => res));
  };
}
