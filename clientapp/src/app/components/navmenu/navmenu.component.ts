import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { NgbCollapseModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-navmenu',
  standalone: true,
  imports: [NgbCollapseModule],
  templateUrl: './navmenu.component.html',
  styleUrl: './navmenu.component.css',
})
export class NavmenuComponent implements OnInit {
  isCollapsed = true;
  isAuthenticated: boolean = false;

  constructor(
    @Inject(DOCUMENT) public document: Document,
    public auth: AuthService
  ) {}

  ngOnInit(): void {
    this.auth.isAuthenticated$.subscribe((authenticated) => {
      this.isAuthenticated = authenticated;
    });
  }

  loginWithRedirect() {
    this.auth.loginWithRedirect();
  }

  logout() {
    this.auth.logout({
      logoutParams: { returnTo: this.document.location.origin },
    });
  }
}
