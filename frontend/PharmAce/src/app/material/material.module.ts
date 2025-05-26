import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule }   from '@angular/material/sidenav';
import { MatListModule }      from '@angular/material/list';
import { MatTableModule }     from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule }      from '@angular/material/sort';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule }       from '@angular/material/card';
import { MatExpansionModule }  from '@angular/material/expansion';
import { MatOption } from '@angular/material/select';
import { MatSidenav } from '@angular/material/sidenav';
import { MatBadgeModule } from '@angular/material/badge'; 
import { MatSelectModule } from '@angular/material/select';
import { MatInput } from '@angular/material/input';
import { MatCard } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { ReactiveFormsModule } from '@angular/forms';


const MATERIAL_MODULES = [
  MatButtonModule,MatCard,
  MatToolbarModule,MatSelectModule ,MatInput,
  MatIconModule,MatProgressSpinnerModule,  MatSidenavModule,
  MatToolbarModule,
  MatIconModule,
  MatListModule,ReactiveFormsModule,
  MatButtonModule,
  MatTableModule,
  MatPaginatorModule,
  MatSortModule,
  MatFormFieldModule,MatAutocompleteModule,
  MatInputModule,
  MatDialogModule,
  MatMenu , MatMenuTrigger,
  MatCardModule , MatExpansionModule,MatOption , MatSidenav , MatBadgeModule

];

@NgModule({
  declarations: [],
  imports: MATERIAL_MODULES,
  exports: MATERIAL_MODULES,
})
export class MaterialModule { }
