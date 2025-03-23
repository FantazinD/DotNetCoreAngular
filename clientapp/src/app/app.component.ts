import { LoadingComponent } from './components/shared/loading/loading.component';
import { AdminAuthGuardService } from '../../services/admin-auth-guard.service';
import { NavmenuComponent } from './components/navmenu/navmenu.component';
import { CustomAuthService } from '../../services/custom-auth.service';
import { HomeComponent } from './components/home/home.component';
import { VehicleService } from '../../services/vehicle.service';
import { ConfigService } from '../../services/config.service';
import { PhotoService } from '../../services/photo.service';
import { Component, ErrorHandler } from '@angular/core';
import { AppErrorHandler } from './app.error-handler';
import { AuthService } from '@auth0/auth0-angular';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    HomeComponent,
    NavmenuComponent,
    FormsModule,
    CommonModule,
    LoadingComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  providers: [
    { provide: ErrorHandler, useClass: AppErrorHandler },
    VehicleService,
    PhotoService,
    CustomAuthService,
    AdminAuthGuardService,
  ],
})
export class AppComponent {
  title: string = 'Vega';

  constructor(public auth: AuthService) {}
}

export function initConfig(configService: ConfigService) {
  return () =>
    firstValueFrom(configService.loadConfig()).then((config) => {
      configService.setConfig(config);
    });
}
