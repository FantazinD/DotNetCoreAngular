import { Component } from '@angular/core';
import { NavmenuComponent } from '../navmenu/navmenu.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [NavmenuComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {}
