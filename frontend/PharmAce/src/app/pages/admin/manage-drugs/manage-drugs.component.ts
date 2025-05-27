import { CommonModule } from '@angular/common';
import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../../services/api.service';
import { CategoryDto } from '../../../models/categoryDto.model';
import { MaterialModule } from '../../../material/material.module';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-manage-drugs',
  imports: [CommonModule, FormsModule, MaterialModule , FormsModule],
  standalone: true,

  templateUrl: './manage-drugs.component.html',
  styleUrl: './manage-drugs.component.scss'
})
export class ManageDrugsComponent implements OnInit, OnChanges  {
  @Input() searchQuery: string = '';

  drugs: any[] = [];
  totalCount: number = 0;
  showAddForm = false;
  editMode = false;
  editingDrugId: string | null = null;
  // name : string = ''; 
  // description: string = '';
  // stock : number = 0;
  // price : number = 0;
  newDrug: any = {
    name: '',
    stock: undefined,
    description: '',
    price: undefined,
    drugExpiry: null,
    categoryId:null,
    SupplierId:''
  };
  SupplierId: string | null ='';
  num:string='';
  categories: CategoryDto[] = [];

  constructor(
    private apiService: ApiService,
    private snackBar: MatSnackBar,
  ) {}

  ngOnInit(): void {
    if (!this.searchQuery) {      
      this.fetchAllDrugs();
      this.getCategories();
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['searchQuery'] && !changes['searchQuery'].firstChange) {
      //const newSearchTerm = changes['searchQuery'].currentValue;
      //this.fetchDrugs(newSearchTerm);
      this.fetchAllDrugs();
    }
  }
  fetchAllDrugs(): void {
    this.apiService.getDrugs().subscribe({
      next: response => {
        this.drugs = Array.isArray(response) ? response : response.items || [];
        console.log('Fetched Drugs:', this.drugs); // Check the console for the drug object structure
      },
      error: err => {
        console.error('Error loading drugs:', err);
      }
    });
  }

  getCategories(): void {
    this.apiService.getCategories().subscribe({
      next: (categories) => {
        this.categories = categories;
      },
      error: (err) => {
        console.error('Error fetching categories:', err);
        this.snackBar.open('Failed to load categories', 'Close', { duration: 3000 });
      }
    });
  }

  getCategoryName(categoryId: string): string {
    const category = this.categories.find(c => c.categoryId === categoryId);
    return category ? category.categoryName : 'Uncategorized';
  }
  
  fetchDrugs(searchTerm: string = '', page: number = 1, pageSize: number = 10): void {
    this.apiService.getFilteredDrugs(searchTerm, page, pageSize).subscribe({
      next: response => {
        this.drugs = response.items;
        this.totalCount = response.totalCount;
        
      },
      error: err => {
        console.error('Error fetching drugs:', err);
      }
    });
  }

  // ✅ In addDrug(), adjust validation and payload
  addDrug(): void {
    const userId = this.apiService.GetuserId();
    this.newDrug.SupplierId = userId;
    if (!this.newDrug.name || !this.newDrug.price || this.newDrug.stock==undefined || this.newDrug.stock==0 || !this.newDrug.categoryId || !this.newDrug.SupplierId || !this.newDrug.drugExpiry) {
      this.snackBar.open('Please fill in all required fields.', 'Close', { duration: 3000 });
      return;
    }

    this.apiService.addDrug(this.newDrug).subscribe({
      next: (data) => {
        this.snackBar.open('Drug added successfully!', 'Close', { duration: 3000 });
        //this.fetchDrugs(); // better than pushing manually
        this.fetchAllDrugs();
        //this.newDrug = { name: '', description: '', price: 0, categoryName: '' };
        this.newDrug = { name: '', description: '', price: 0 ,stock:0,drugExpiry:null,categoryId: null};
        this.showAddForm = false;

        const drugobj: any={
          drugId: data.result.drugId,
          name: data.result.name,
          description: data.result.description,
          stock: data.result.stock,
          drugExpiry: data.result.drugExpiry,
          price: data.result.price,
          categoryId: data.result.categoryId,
          supplierId: data.result.supplierId
        };
        this.apiService.addDrugInInventory(drugobj).subscribe();
      },
      error: (err) => {
        console.error('Add drug failed:', err);
        this.snackBar.open('Failed to add drug.', 'Close', { duration: 3000 });
      }
    });
  }


  // ✅ In editDrug()
  editDrug(drug: any): void {
    this.SupplierId = this.apiService.GetuserId();
    // this.newDrug.userId = userId;
    this.newDrug = {
      drugId:drug.drugId,
      name: drug.name,
      description: drug.description,
      price: drug.price,
      stock: drug.stock,
      drugExpiry:drug.drugExpiry,
      categoryId:drug.categoryId,
      SupplierId:this.SupplierId
    };
    this.editMode = true;
    this.editingDrugId = drug.drugId;
    this.showAddForm = true;
  }


  // updateDrug(): void {
  //   //if (!this.editingDrugId) return;

  //   this.apiService.updateDrug( this.newDrug).subscribe({
  //     next: () => {
  //       this.snackBar.open('Drug updated successfully!', 'Close', { duration: 3000 });
  //       //this.fetchDrugs();
  //       this.fetchAllDrugs();
  //       this.cancelForm();
  //     },
  //     error: (err) => {
  //       console.error('Update failed:', err);
  //       this.snackBar.open('Failed to update drug.', 'Close', { duration: 3000 });
  //     }
  //   });
  // }
  updateDrug(): void {
    if (!this.editingDrugId) return;
    let drug=this.newDrug;
    // console.log(drug);
    this.apiService.updateDrug(drug).subscribe({
      next: () => {
        this.snackBar.open('Drug updated successfully!', 'Close', { duration: 3000 });
        this.fetchDrugs();
        this.cancelForm();
        
        this.apiService.updateDrugInInventory(drug).subscribe();
        this.fetchAllDrugs();
      },
      error: err => {
        console.error('Update failed:', err);
        this.snackBar.open('Failed to update drug.', 'Close', { duration: 3000 });
      }
    });
  }

  deleteDrug(id: string): void {
    Swal.fire({
    title: 'Confirm Deletion',
    text: 'Are you sure you want to delete this Drug?',
    icon: 'warning',
    background: '#ffffff',
    color: '#1e3a8a', 
    showCancelButton: true,
    confirmButtonColor: '#1e40af', 
    cancelButtonColor: '#e5e7eb', 
    confirmButtonText: 'Yes, Delete',
    cancelButtonText: 'Cancel',
    customClass: {
      popup: 'rounded-xl shadow-lg',
      title: 'text-xl font-semibold',
      confirmButton: 'px-4 py-2',
      cancelButton: 'px-4 py-2',
    }
  }).then((result) => {
    if (result.isConfirmed) {
    {
      this.apiService.deleteDrug(id).subscribe({
        next: () => {
          Swal.fire({
            title: 'Deleted!',
            text: 'User has been successfully deleted.',
            icon: 'success',
            background: '#ffffff',
            color: '#1e3a8a',
            confirmButtonColor: '#1e40af',
            confirmButtonText: 'OK',
            customClass: {
              popup: 'rounded-xl shadow-md',
              title: 'text-lg font-medium',
              confirmButton: 'px-4 py-2'
            }
          });
          this.snackBar.open('Drug deleted.', 'Close', { duration: 3000 });
          this.drugs = this.drugs.filter(d => d.id !== id);
          this.fetchAllDrugs();
        },
        error: (err) => {
          console.error('Delete failed:', err);
          Swal.fire({
            title: 'Error!',
            text: 'Failed to delete user.',
            icon: 'error',
            background: '#ffffff',
            color: '#b91c1c', 
            confirmButtonColor: '#1e40af',
            confirmButtonText: 'OK',
          });
        }
      });
      }
    };
  });
}


  // deleteDrugByName(name: string) {
  //   if (!confirm('Are you sure?')) return;
  //   this.apiService.deleteDrugByName(name).subscribe({
  //     next: () => {
  //       this.snackBar.open('Deleted.', 'Close', { duration: 2000 });
  //       this.fetchDrugs();
  //     },
  //     error: err => { /*…*/ }
  //   });
  // }
  
  // ✅ In cancelForm()
  cancelForm(): void {
    this.showAddForm = false;
    this.editMode = false;
    this.editingDrugId = null;
    this.newDrug = { name: '', description: '', price: null,stock:null,drugExpiry:null,categoryId: null,SupplierId:''};
  }


}
// import { CommonModule } from '@angular/common';
// import { Component } from '@angular/core';
// import { FormsModule } from '@angular/forms';
// import { ApiService } from '../../../services/api.service';
// //import { MatSnackBar } from '@angular/material/snack-bar';

// @Component({
//   selector: 'app-manage-drugs',
//   imports: [  FormsModule , CommonModule],
//   templateUrl: './manage-drugs.component.html',
//   styleUrl: './manage-drugs.component.scss'
// })
// export class ManageDrugsComponent {
//   drugs: any[] = [];
//   totalCount: number = 0;
//   showAddForm = false;
//   editMode = false;
//   editingDrugId: string | null = null;

//   newDrug: any = {
//     name: '',
//     manufacturer: '',
//     description: '',
//     categoryName: '',
//     price: 0
//   };

//   constructor(
//     private apiService: ApiService,
//     // private snackBar: MatSnackBar,
//   ) {}
//   addDrug(): void {
//     // if (!this.newDrug.name || !this.newDrug.price || !this.newDrug.categoryName) {
//     //   this.snackBar.open('Please fill in all required fields.', 'Close', { duration: 3000 });
//     //   return;
//     // }

//     this.apiService.addDrug(this.newDrug).subscribe({
//       next: (data) => {
//         //this.snackBar.open('Drug added successfully!', 'Close', { duration: 3000 });
//         //this.fetchDrugs(); // better than pushing manually
//         this.newDrug = { name: '', description: '', price: 0, categoryName: '' };
//         this.showAddForm = false;
//       },
//       error: (err) => {
//         console.error('Add drug failed:', err);
//         //this.snackBar.open('Failed to add drug.', 'Close', { duration: 3000 });
//       }
//     });
//   }
// }
