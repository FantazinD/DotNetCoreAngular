import { CommonModule, DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthService, IdToken, User } from '@auth0/auth0-angular';
import { NgbCollapseModule } from '@ng-bootstrap/ng-bootstrap';
import { combineLatest, filter, mergeMap, switchMap } from 'rxjs';

@Component({
  selector: 'app-navmenu',
  standalone: true,
  imports: [NgbCollapseModule, RouterModule, CommonModule],
  templateUrl: './navmenu.component.html',
  styleUrl: './navmenu.component.css',
})
export class NavmenuComponent {
  isCollapsed = true;

  constructor(
    @Inject(DOCUMENT) public document: Document,
    public auth: AuthService
  ) {}

  loginWithRedirect() {
    this.auth.loginWithRedirect();
  }

  logout() {
    this.auth.logout({
      logoutParams: { returnTo: this.document.location.origin },
    });
  }
}
