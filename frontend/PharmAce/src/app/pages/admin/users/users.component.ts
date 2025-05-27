import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UsersService } from '../../../services/users.service';
import { Users } from '../../../models/user.model';
import { MaterialModule } from '../../../material/material.module';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import Swal from 'sweetalert2';
import { ApiService } from '../../../services/api.service';

@Component({
  selector: 'app-users',
  imports: [CommonModule , ReactiveFormsModule ,FormsModule ,  MaterialModule],
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss'
})
export class UsersComponent {

  users: Users[] = [];
  searchQuery: string = '';
  showAddForm = false;
  editMode = false;
  newUser: Users = this.getEmptyUser();
  
  display: string[] = ['userId', 'userName', 'email', 'phoneNumber', 'role', 'actions'];

  constructor(private userService: UsersService , private snackbar: MatSnackBar , private apiService : ApiService) {}

  ngOnInit(): void {
    this.fetchUsers();
  }

  getEmptyUser(): Users {
    return {
      userId: '' , // userId can be null or empty string for new users
      userName: '',
      email: '',
      phoneNumber: '',
      role: 'Doctor' // default role
    };
  }

  fetchUsers() {
    this.userService.getAllUsers().subscribe({
      next: (res) => this.users = res,
      error: (err) => console.error('Failed to fetch users', err)
    });
  }
  toggleAddForm() {
    this.showAddForm = !this.showAddForm;
    if (!this.showAddForm) {
      this.newUser = this.getEmptyUser();
      this.editMode = false;
    }
  }

 
  addUser() {
    console.log(this.newUser);
  if (!this.newUser.userName || !this.newUser.email || !this.newUser.phoneNumber || !this.newUser.role) {
    this.snackbar.open('Please fill in all required fields.', 'Close', { duration: 3000 });
    return;
  }
  const userToSend = { ...this.newUser };
  delete userToSend.userId;

  this.userService.addUser(userToSend).subscribe({
    next: () => {
      this.snackbar.open('User added successfully!', 'Close', { duration: 3000 });
      this.fetchUsers();
      this.newUser = this.getEmptyUser();
      this.showAddForm = false;
      console.log(this.newUser);
    },
    error: (err: HttpErrorResponse) => {
      console.error('Failed to add user', err);
      this.snackbar.open('Failed to add user.', 'Close', { duration: 3000 });
    }
  });
}


  
deleteUser(id: string): void {
  Swal.fire({
    title: 'Confirm Deletion',
    text: 'Are you sure you want to delete this user?',
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
      this.userService.deleteUser(id).subscribe({
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
          this.users = this.users.filter(d => d.userId !== id);
          this.fetchUsers();
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
  });
}

  //   deleteUser(id: string): void {
  //   if (confirm('Are you sure you want to delete this User?')) {
  //     this.userService.deleteUser(id).subscribe({
  //       next: () => {
  //         this.snackbar.open('User deleted.', 'Close', { duration: 3000 });
  //         this.users = this.users.filter(d => d.userId !== id);
  //         this.fetchUsers();
  //       },
  //       error: (err) => {
  //         console.error('Delete failed:', err);
  //       }
  //     });
  //   }
  // }

  
  // addUser() {
  //   this.userService.addUser(this.newUser).subscribe({
  //     next: () => {
  //       this.fetchUsers();
  //       this.showAddForm = false;
  //       this.newUser = this.getEmptyUser();
  //     },
  //     error: (err) => console.error('Failed to add user', err)
  //   });
  // }

  editUser(user: Users) {
    this.newUser = {
      userId: user.userId,
      userName: user.userName,
      email: user.email,
      phoneNumber: user.phoneNumber,
      role: user.role
    };
    this.editMode = true;
    this.showAddForm = true;
    this.newUser = { ...user };
  }

  updateUser() {
    this.userService.updateUsers(this.newUser).subscribe({
      next: () => {
        this.fetchUsers();
        this.editMode = false;
        this.showAddForm = false;
        this.newUser = this.getEmptyUser();
      },
      error: (err: HttpErrorResponse) => console.error('Failed to update user', err)
    });
  }

  
}
