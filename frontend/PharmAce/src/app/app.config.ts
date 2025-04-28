import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';

export const appConfig: any = {
  providers: [provideZoneChangeDetection({ eventCoalescing: true }), provideRouter(routes)],
  apiUrl : 'http://localhost:5254/api',
};
