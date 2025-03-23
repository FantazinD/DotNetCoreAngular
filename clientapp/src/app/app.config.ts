import {
  APP_INITIALIZER,
  ApplicationConfig,
  provideZoneChangeDetection,
} from '@angular/core';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideCharts, withDefaultRegisterables } from 'ng2-charts';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { provideClientHydration } from '@angular/platform-browser';
import { ConfigService } from '../../services/config.service';
import { provideAuth0 } from '@auth0/auth0-angular';
import { provideRouter } from '@angular/router';
import { initConfig } from './app.component';
import { provideToastr } from 'ngx-toastr';
import { routes } from './app.routes';

export const appConfig: ApplicationConfig = {
  providers: [
    provideAuth0({
      domain: 'dev-fantazindy-vega.jp.auth0.com',
      clientId: 'DHYESkSW0OMp7cBoRrpmn46Fc0712trk',
      authorizationParams: {
        redirect_uri: window.location.origin,
      },
      useRefreshTokens: true,
      cacheLocation: 'localstorage',
    }),
    {
      provide: APP_INITIALIZER,
      useFactory: initConfig,
      deps: [ConfigService],
      multi: true,
    },
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideClientHydration(),
    provideHttpClient(withFetch()),
    provideToastr(),
    provideAnimations(),
    provideCharts(withDefaultRegisterables()),
  ],
};
