import { Component, ErrorHandler } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { FormsModule } from '@angular/forms';
import { NavmenuComponent } from './components/navmenu/navmenu.component';
import { VehicleService } from '../../services/vehicle.service';
import { AppErrorHandler } from './app.error-handler';
import { PhotoService } from '../../services/photo.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HomeComponent, NavmenuComponent, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  providers: [
    VehicleService,
    PhotoService,
    { provide: ErrorHandler, useClass: AppErrorHandler },
  ],
})
export class AppComponent {
  title: string = 'Vega';
}
