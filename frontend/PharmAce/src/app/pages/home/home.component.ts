import { Component, OnInit, SimpleChanges } from '@angular/core';
import { CommonModule, NgClass } from '@angular/common';
import { DrugListComponent } from '../../components/drug-list/drug-list.component';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { CategoryService } from '../../services/category.service';
import { DrugService } from '../../services/drug.service';
import { CartService } from '../../services/cart.service';
import { Category } from '../../models/category.model';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Drug } from '../../models/drug.model';
import { AuthService } from '../../services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  imports: [
    CommonModule,
    FormsModule,
    NavbarComponent,
    RouterLink,
    DrugListComponent,
    FooterComponent 
  ],
})
export class HomeComponent implements OnInit {
  searchQuery: string = '';
  categories: Category[] = [];
  searchInputValue: string = '';
  featuredDrugs: Drug[] = [];
  cartCount: number = 0;
  drugs: any[] = [];
  filteredDrugs: any[] = [];
  UserId : string ='';
  cartCount$ : any;
  private categoryIcons: { [key: string]: string } = {
    'Stomach ache': 'bi-stomach',
    'Stomach pain': 'bi-stomach',
    'Liver': 'bi-organ',
    'Pain Reliever': 'bi-pain',
    'Headache': 'bi-head-side-virus',
    'Antibiotic': 'bi-capsule',
    'Cold & Flu': 'bi-thermometer-snow',
    'Allergy': 'bi-flower1',
    'Vitamins': 'bi-capsule-pill'
  };

  constructor(
    private categoryService: CategoryService,
    private drugService: DrugService,
    private cartService: CartService,
    private router: Router,
    public authService: AuthService,
    private snackBar: MatSnackBar,
    private route : ActivatedRoute
  ) {}

  ngOnInit(): void {
    
    
    this.fetchCategories();
    this.loadFeaturedDrugs();
    this.updateCartCount();
    this.UserId=this.authService.getUserIdFromToken()!;
    this.cartCount$ = this.cartService.cartCount$;
  }
 
  testimonials = [
    { name: 'John Doe', message: 'Great service and fast delivery!' },
    { name: 'Jane Smith', message: 'The medicines are genuine and affordable.' },
    { name: 'Dr. Alex', message: 'Highly recommend PharmAce for all your needs.' }
  ];
  
  newsletterEmail: string = '';
  
  subscribeToNewsletter(): void {
    if (this.newsletterEmail.trim()) {
      console.log(`Subscribed with email: ${this.newsletterEmail}`);
      alert('Thank you for subscribing!');
      this.newsletterEmail = '';
    } else {
      alert('Please enter a valid email address.');
    }
  }

  handleAuthAction(): void {
    if (this.authService.isLoggedIn()) {
      this.authService.logout();
      this.router.navigate(['/login']); // Redirect to login page after logout
    } else {
      this.router.navigate(['/login']); // Redirect to login page
    }
  }

  loadFeaturedDrugs(): void {
    this.drugService.getDrugs('', 1, 8).subscribe({
      next: (response) => {
        this.drugs = response.items; 
        this.featuredDrugs = response.items.slice(0, 4);
        this.applySearch();
        //this.drugs = response.items; // Store all drugs for filtering
      },
      error: (error) => console.error('Error loading featured drugs:', error)
    });
  }

  fetchCategories(): void {
    this.categoryService.getCategories().subscribe({
      next: (categories) => {
        this.categories = categories.map(category => ({
          ...category,
          icon: this.categoryIcons[category.categoryName] || 'bi-capsule'
        }));
      },
      error: (error) => console.error('Error fetching categories:', error)
    });
  }

  

  addToCart(drug: Drug): void {
    this.cartService.addToCart(drug , 1, this.UserId);
    this.updateCartCount();
    this.snackBar.open(`${drug.name} added to cart`, 'Close', {
      duration: 3000,
    });

    // You can add a toast notification here
  }

  private updateCartCount(): void {
    this.cartCount = this.cartService.getCartCount();
  }

  // onCategoryClick(categoryName: string): void {
  //   this.router.navigate(['/medicines'], {
  //     queryParams: { category: categoryName }
  //   });
  // }
  onCategoryClick(categoryName: string): void {
    this.router.navigate([], {
      queryParams: { category: categoryName },
      queryParamsHandling: 'merge' // to keep other params
    });
  }
  
  viewAllMedicines(): void {
    this.router.navigate(['/medicines']);
  }

  onSearch(): void {
    if (this.searchInputValue.trim()) {
      this.router.navigate(['/medicines'], {
        queryParams: { search: this.searchInputValue.trim() }
      });
    }
  }
  getCategoryName(categoryId: string): string {
    const category = this.categories.find(c => c.categoryId === categoryId);
    return category ? category.categoryName : 'Uncategorized';
  }

  applySearch(): void {
    const term = this.searchQuery.toLowerCase().trim();
  
    if (!term) {
      this.filteredDrugs = this.drugs.slice(0, 8); // show top 8 when empty
      return;
    }
  
    this.filteredDrugs = this.drugs.filter(drug =>
      drug.name.toLowerCase().includes(term) ||
      (drug.description?.toLowerCase().includes(term)) ||
      this.getCategoryName(drug.categoryId).toLowerCase().includes(term)
    ).slice(0, 8); // limit results
  }
  
}

