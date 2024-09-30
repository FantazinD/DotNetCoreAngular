import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { VehicleFormComponent } from './components/vehicle-form/vehicle-form.component';
import { VehicleListComponent } from './components/vehicle-list/vehicle-list.component';
import { ViewVehicleComponent } from './components/view-vehicle/view-vehicle.component';
import { AdminComponent } from './components/admin/admin.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'vehicles/new', component: VehicleFormComponent },
  { path: 'vehicles/edit/:id', component: VehicleFormComponent },
  { path: 'vehicles/:id', component: ViewVehicleComponent },
  { path: 'vehicles', component: VehicleListComponent },
  { path: 'admin', component: AdminComponent },
  { path: 'home', component: HomeComponent },
];
