import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        loadComponent: () =>
          import('./components/auth/auth.component').then((m) => m.AuthComponent),
      }
];
