import { CommonModule, DOCUMENT } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';
import { CustomAuthService } from '../../../../services/custom-auth.service';

@Component({
  selector: 'app-navmenu',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './navmenu.component.html',
  styleUrl: './navmenu.component.css',
})
export class NavmenuComponent {
  isCollapsed = true;

  constructor(
    @Inject(DOCUMENT) public document: Document,
    public auth: AuthService,
    public customAuth: CustomAuthService
  ) {}

  toggleMenu = () => {
    this.isCollapsed = !this.isCollapsed;
  };

  clickNavItem = () => {
    this.isCollapsed = true;
  };
}
