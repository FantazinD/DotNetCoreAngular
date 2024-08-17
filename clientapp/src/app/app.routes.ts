import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { VehicleFormComponent } from './components/vehicle-form/vehicle-form.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'vehicles/new', component: VehicleFormComponent },
  { path: 'home', component: HomeComponent },
];
