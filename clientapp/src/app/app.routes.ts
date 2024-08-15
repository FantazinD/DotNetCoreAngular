import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { AboutComponent } from './components/about/about.component';
import { ContactComponent } from './components/contact/contact.component';
import { VehicleFormComponent } from './components/vehicle-form/vehicle-form.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'vehicles/new', component: VehicleFormComponent },
  { path: 'about', component: AboutComponent },
  { path: 'contact', component: ContactComponent },
];
