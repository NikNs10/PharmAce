import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MaterialModule } from '../../../material/material.module';
import { Router } from '@angular/router';
import { UsersService } from '../../../services/users.service';
import { Observable } from 'rxjs';


@Component({
  selector: 'app-suppliers',
  imports: [CommonModule , MaterialModule],
  templateUrl: './suppliers.component.html',
  styleUrl: './suppliers.component.scss'
})
export class SuppliersComponent {
  Suppliers: any[] = [];
  display = ['supplierId', 'supplierName' , 'supplierEmail', 'supplierPhone'];

  constructor(
    private router: Router,
    private userService: UsersService,
  ) {  }

  ngOnInit(): void {
    this.getSuppliers();

  }

  getSuppliers(): any {
    this.userService.getAllUsers().subscribe({
      next: (response) => {
        this.Suppliers = response.filter((user: any) => user.role === 'Supplier');
        console.log(this.Suppliers);
      },
      error: () => {
        console.error('Error fetching suppliers');
      }
    });
  }
}
