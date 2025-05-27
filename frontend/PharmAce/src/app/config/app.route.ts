import { Routes } from '@angular/router';
import { AdminGuard } from '../guards/admin.guard';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('../pages/home/home.component').then((m) => m.HomeComponent),
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
    path: 'cart',
    loadComponent: () =>
      import('../components/cart/cart.component').then((m) => m.CartComponent),
  },
  {
    path: 'checkout',
    loadComponent: () =>
      import('../components/checkout/checkout.component').then((m) => m.CheckoutComponent),
  },
  {
    path: 'admin/dashboard',
    loadComponent: () => 
      import('../pages/admin/dashboard/dashboard.component').then((m) => m.DashboardComponent),
    
      canActivate: [AdminGuard],
      children: [
        {
          path: '',
          loadComponent: () =>
            import('../pages/admin/home/home.component').then((m) => m.HomeComponent),
        },
        {
          path: 'home',
          loadComponent: () =>
            import('../pages/admin/home/home.component').then((m) => m.HomeComponent),
        },
        {
          path: 'manage-drugs',
          loadComponent: () => 
            import('../pages/admin/manage-drugs/manage-drugs.component').then((m) => m.ManageDrugsComponent),

        },
        {
          path: 'orders',
          loadComponent: () => 
            import('../pages/admin/orders/orders.component').then((m) => m.OrdersComponent),

        },
        {
          path: 'suppliers',
          loadComponent: () => 
            import('../pages/admin/suppliers/suppliers.component').then((m) => m.SuppliersComponent),
        },
        {
          path: 'manage-users',
          loadComponent: () => 
            import('../pages/admin/users/users.component').then((m) => m.UsersComponent),          
        }
      ]
  },
  { 
    path: '**', 
    redirectTo: '' 
  }
  
];
