import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { FormsModule } from '@angular/forms';
import { NavmenuComponent } from './components/navmenu/navmenu.component';
import { MakeService } from '../../services/make.service';
import { FeatureService } from '../../services/feature.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HomeComponent, NavmenuComponent, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  providers: [MakeService, FeatureService],
})
export class AppComponent {
  title: string = 'Vega';
}
