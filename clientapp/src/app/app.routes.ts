import { VehicleFormComponent } from './components/vehicle-form/vehicle-form.component';
import { VehicleListComponent } from './components/vehicle-list/vehicle-list.component';
import { ViewVehicleComponent } from './components/view-vehicle/view-vehicle.component';
import { AdminAuthGuardService } from '../../services/admin-auth-guard.service';
import { AdminComponent } from './components/admin/admin.component';
import { HomeComponent } from './components/home/home.component';
import { AuthGuard } from '@auth0/auth0-angular';
import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: 'vehicles/new',
    component: VehicleFormComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'vehicles/edit/:id',
    component: VehicleFormComponent,
    canActivate: [AuthGuard],
  },
  { path: 'vehicles/:id', component: ViewVehicleComponent },
  { path: 'vehicles', component: VehicleListComponent },
  {
    path: 'admin',
    component: AdminComponent,
    canActivate: [AdminAuthGuardService],
  },
  { path: 'home', component: HomeComponent },
];
