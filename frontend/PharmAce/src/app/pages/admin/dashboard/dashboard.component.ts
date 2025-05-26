import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterLink, RouterModule } from '@angular/router';
import { MaterialModule } from '../../../material/material.module';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule , RouterModule , RouterLink , MaterialModule  ],
  standalone: true,
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {
  title = "Dashboard";
  isSidebarOpen = false;

  constructor(private route: Router , private authService: AuthService) { }

  toggleSidebar() {
    this.isSidebarOpen = !this.isSidebarOpen;
  }
  
  logout() {
    this.authService.logout(); // Call the logout method from AuthService    
    this.route.navigate(['']); // Redirect to the login page after logout
  }

  goToProfile() { 
    // Logic to navigate to the profile page
    console.log('Navigating to profile page...');


    // You can use Angular Router to navigate to the profile page if needed

  }
}


// import { Component, OnInit, ViewChild } from '@angular/core';
// import { HttpClient }                     from '@angular/common/http';
// import { Router }                         from '@angular/router';
// import { MatSidenav }                     from '@angular/material/sidenav';
// import { MatCard, MatCardContent, MatCardHeader, MatCardTitle } from '@angular/material/card';
// import { MatInputModule } from '@angular/material/input';
// import { RouterLink, RouterModule } from '@angular/router';
// import { MatSidenavModule }   from '@angular/material/sidenav';
// import { MatToolbarModule }   from '@angular/material/toolbar';
// import { MatIconModule }      from '@angular/material/icon';
// import { MatListModule }      from '@angular/material/list';
// import { MatButtonModule }    from '@angular/material/button';
// import { MatTableModule }     from '@angular/material/table';
// import { MatPaginatorModule } from '@angular/material/paginator';
// import { MatSortModule }      from '@angular/material/sort';
// import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
// import { MatFormFieldModule } from '@angular/material/form-field';
// import { MatDialogModule } from '@angular/material/dialog';
// import { CommonModule } from '@angular/common';

// interface MetricPayload {
//   ordersToday:   number;
//   revenueMonth:  number;
//   lowStockCount: number;
//   newUsers:      number;
// }

// interface StockItem { name: string; sku: string; qty: number; }
// interface OrderItem { id: string; customer: string; status: string; total: number; date: string; }

// @Component({
//   selector: 'app-dashboard',
//   templateUrl: './dashboard.component.html',
//   styleUrls: ['./dashboard.component.scss'],
//   standalone: true,
//   imports: [
//     MatSidenav, MatCard ,     MatSidenavModule,
//     MatToolbarModule,
//     MatIconModule,
//     MatListModule,
//     MatButtonModule,
//     MatTableModule,
//     MatPaginatorModule,
//     MatSortModule,
//     MatFormFieldModule,
//     MatInputModule,
//     MatDialogModule, CommonModule,
//     MatMenu , MatMenuTrigger , RouterModule  ,MatCardHeader , MatCardTitle , RouterLink,MatCardContent,
//     // other Angular Material modules you need
//   ] 
// })
// export class DashboardComponent implements OnInit {
//   @ViewChild('drawer') sidenav!: MatSidenav;

//   metrics?: MetricPayload;
//   lowStock: StockItem[]    = [];
//   recentOrders: OrderItem[] = [];

//   lowStockColumns = ['name','sku','qty','action'];
//   orderColumns    = ['orderId','customer','status','total','date'];

//   constructor(private http: HttpClient, private router: Router) {}

//   ngOnInit() {
//     // 1. Top-level metrics
//     this.http.get<MetricPayload>('/api/admin/metrics')
//       .subscribe(m => this.metrics = m);

//     // 2. Low-stock items
//     this.http.get<StockItem[]>('/api/admin/low-stock')
//       .subscribe(data => this.lowStock = data);

//     // 3. Recent orders
//     this.http.get<OrderItem[]>('/api/admin/recent-orders')
//       .subscribe(data => this.recentOrders = data);

//     // 4. (Later) sales-trend for chart
//     // this.http.get<YourChartPoint[]>('/api/admin/sales-trend').subscribe(...)
//   }

//   // navigation & actions
//   viewDetail(section: string) {
//     this.router.navigate([`/admin/${section}`]);
//   }
//   reorder(sku: string) {
//     // TODO: POST to /api/admin/reorder/:sku
//     console.log('Reorder SKU:', sku);
//   }
//   goToProfile() {
//     this.router.navigate(['/profile']);
//   }
//   logout() {
//     // TODO: integrate your AuthService.logout()
//     this.router.navigate(['/']);
//   }
// }
