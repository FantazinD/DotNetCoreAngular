import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavmenuComponent } from './components/navmenu/navmenu.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavmenuComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'Vega';
}
