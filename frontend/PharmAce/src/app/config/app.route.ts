import { Routes } from '@angular/router';
import { AdminGuard } from '../guards/admin.guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full' // VERY important for redirect
  },
  {
    path: 'login',
    loadComponent: () =>
      import('../pages/auth/login/login.component').then((m) => m.LoginComponent),
  },
  {
    path: 'signup',
    loadComponent: () =>
      import('../pages/auth/signup/signup.component').then((m) => m.SignupComponent),
  },
  {
    path: 'admin/dashboard',
    loadComponent: () => 
      import('../pages/admin/dashboard/dashboard.component').then((m) => m.DashboardComponent),
      canActivate: [AdminGuard],
      children: [
        {
          path: 'manage-drugs',
          loadComponent: () => 
            import('../pages/admin/manage-drugs/manage-drugs.component').then((m) => m.ManageDrugsComponent),

        }
      ]
  },
  { 
    path: '**', 
    redirectTo: 'login' 
  }
  
];
