import { DOCUMENT } from '@angular/common';
import { Inject, Injectable } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { filter, switchMap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CustomAuthService {
  private roles: string[] = [];

  constructor(
    @Inject(DOCUMENT) public document: Document,
    public auth: AuthService
  ) {
    this.auth.isAuthenticated$
      .pipe(
        filter((isAuthenticated) => isAuthenticated),
        switchMap(() => this.auth.idTokenClaims$)
      )
      .subscribe((idTokenClaims) => {
        localStorage.setItem('token', idTokenClaims!.__raw);
        this.roles = idTokenClaims!['https://vegafanta.com/roles']['roles'];
      });
  }

  isInRole = (roleName: string) => {
    return this.roles.indexOf(roleName) > -1;
  };

  login = () => {
    this.auth.loginWithRedirect();
  };

  logout = () => {
    this.auth.logout({
      logoutParams: { returnTo: this.document.location.origin },
    });
    localStorage.removeItem('token');
    this.roles = [];
  };
}
