import { CommonModule } from '@angular/common';
import { Component, ViewChild } from '@angular/core';
import { MaterialModule } from '../../../material/material.module';
import { Router, RouterLink } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { MatSidenav } from '@angular/material/sidenav';
import { DashboardComponent } from "../dashboard/dashboard.component";
import { ApiService } from '../../../services/api.service';
import { OrderService } from '../../../services/order.service';
import { Order } from '../../../models/order.model';
import { Drug } from '../../../models/drug.model';



interface StockItem { name: string; sku: string; qty: number; }

@Component({
  selector: 'app-home',
  imports: [CommonModule, MaterialModule, RouterLink, DashboardComponent],
  standalone: true,
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

  @ViewChild('drawer') sidenav!: MatSidenav;
  constructor(private http: HttpClient,
       private router: Router ,
       private apiService : ApiService , 
       private orderService: OrderService ) {}

  goToProfile() {
        this.router.navigate(['/profile']);
      }

    lowStock: StockItem[]    = [];
    recentOrders: Order[] = [];
  
    lowStockColumns = ['name','sku','qty','action'];
    orderColumns    = ['orderId','customer','total','date'];
    
    TotalOrders: number = 0;
    TotalRevenue: number = 0;
    TotalLowStock: number = 0;
    TotalUsers: number = 0;

    Drugs : Drug[] = [];
    
    ngOnInit() {
      this.fetchAllDrugs();
      this.getTotals();
      this.getLowStock();
      this.getTotalUsers();
    }
  
    // navigation & actions
    viewDetail(section: string) {
      this.router.navigate([`/admin/dashboard/${section}`]);
    }

    reorder(sku: string) {
      // TODO: POST to /api/admin/reorder/:sku
      console.log('Reorder SKU:', sku);
      this.router.navigate(['/admin/dashboard/manage-drugs'], { queryParams: { sku } });
    }

    // 
    getTotals(): void {
      this.orderService.getOrders().subscribe({
        next: (OrderDetails) => {
          this.TotalOrders = OrderDetails.length;
          this.TotalRevenue = OrderDetails.reduce((acc, order) => acc + order.totalAmount, 0);
    
          this.recentOrders = OrderDetails
            .filter((order: any) => order.status === 0)
            .sort((a: any, b: any) => new Date(b.orderDate).getTime() - new Date(a.orderDate).getTime());
        },
        error: () => {
          console.error('Error fetching Orders');
        }
      });
    }
    
    getTotalUsers(): void {
      this.apiService.getUsers().subscribe({
        next: (users) => {
          this.TotalUsers = users.length;
        },
        error: () => {
          console.error('Error fetching Users');
        }
      });
    }

    getLowStock(): void {
      this.apiService.getDrugs().subscribe({
        next: (drugs) => {
          const filtered = drugs
            .filter((drug: any) => drug.stock < 15)
            .sort((a: any, b: any) => a.stock - b.stock) // Ascending order
            .slice(0, 5) // Take only first 5
            .map((drug: any) => ({
              name: drug.name,
              sku: drug.drugId,
              qty: drug.stock
            }));
    
          this.lowStock = filtered;
          this.TotalLowStock = filtered.length;
        },
        error: () => {
          console.error('Error fetching low stock items');
        }
      });
    }
    
    
    fetchAllDrugs(): void {
      this.apiService.getDrugs().subscribe({
        next: response => {
          this.Drugs = Array.isArray(response) ? response : response.items || [];
          console.log('Fetched Drugs:', this.Drugs); 
        },
        error: err => {
          console.error('Error loading drugs:', err);
        }
      });
    }

    logout() {
      // TODO: integrate your AuthService.logout()
      this.router.navigate(['/']);
    }
  }
  

