import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig,  } from './app/config/app.config';
import { AppComponent } from './app/app.component';
import { AuthInterceptor } from './app/interceptors/auth.interceptor';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { APP_CONFIG } from './app/config/app-config.token';
import { provideZoneChangeDetection } from '@angular/core';
import { routes } from './app/config/app.route';
import { provideRouter } from '@angular/router';

bootstrapApplication(AppComponent,  {
  providers: [
    provideRouter(routes),
    provideHttpClient(withInterceptorsFromDi()),
    provideZoneChangeDetection(),
    { provide: APP_CONFIG, useValue: appConfig },
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }// Provide custom config
  ]
})
  .catch((err) => console.error(err));
