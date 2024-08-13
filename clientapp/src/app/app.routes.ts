import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { NavmenuComponent } from './components/navmenu/navmenu.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'about', component: NavmenuComponent },
];
