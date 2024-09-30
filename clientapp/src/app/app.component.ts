import { Component, ErrorHandler, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { FormsModule } from '@angular/forms';
import { NavmenuComponent } from './components/navmenu/navmenu.component';
import { VehicleService } from '../../services/vehicle.service';
import { AppErrorHandler } from './app.error-handler';
import { PhotoService } from '../../services/photo.service';
import { AuthService } from '@auth0/auth0-angular';
import { CommonModule } from '@angular/common';
import { LoadingComponent } from './components/shared/loading/loading.component';
import { CustomAuthService } from '../../services/custom-auth.service';
import { AdminAuthGuardService } from '../../services/admin-auth-guard.service';

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
