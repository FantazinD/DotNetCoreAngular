import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService, IdToken } from '@auth0/auth0-angular';
import { map, Observable, of, switchMap } from 'rxjs';
import { CustomAuthService } from './custom-auth.service';

@Injectable({
  providedIn: 'root',
})
export class AdminAuthGuardService implements CanActivate {
  constructor(
    private auth: AuthService,
    private customAuth: CustomAuthService,
    private router: Router
  ) {}

  canActivate(): Observable<boolean> {
    return this.auth.isAuthenticated$.pipe(
      switchMap((isAuthenticated) => {
        if (!isAuthenticated) {
          this.customAuth.login();
          return of(false);
        }

        return this.auth.idTokenClaims$.pipe(
          map((idTokenClaims: IdToken | null | undefined) => {
            if (
              idTokenClaims!['https://vegafanta.com/roles']['roles'].indexOf(
                'Admin'
              ) > -1
            ) {
              return true;
            } else {
              this.router.navigate(['/vehicles']);
              return false;
            }
          })
        );
      })
    );
  }
}
