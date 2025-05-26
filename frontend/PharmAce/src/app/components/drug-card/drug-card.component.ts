import { Component, Input, Output, EventEmitter } from "@angular/core"
import   { Drug } from "../../models/drug.model"
import   { AuthService } from "../../services/auth.service"

@Component({
  selector: "app-drug-card",
  templateUrl: "./drug-card.component.html",
  styleUrls: ["./drug-card.component.scss"],
  standalone: true,
  imports: [CommonModule , MaterialModule],
})
export class DrugCardComponent {
  @Input() drug!: Drug
  @Output() addToCart = new EventEmitter<Drug>()

  constructor(public authService: AuthService) {}

  onAddToCart(): void {
    this.addToCart.emit(this.drug)
  }

  isExpiringSoon(): boolean {
    const expiryDate = new Date(this.drug.drugExpiry)
    const today = new Date()
    const threeMonthsFromNow = new Date()
    threeMonthsFromNow.setMonth(today.getMonth() + 3)

    return expiryDate <= threeMonthsFromNow
  }

  getStockStatus(): { color: string; text: string } {
    if (this.drug.stock <= 0) {
      return { color: "red", text: "Out of Stock" }
    } else if (this.drug.stock < 10) {
      return { color: "orange", text: "Low Stock" }
    } else {
      return { color: "green", text: "In Stock" }
    }
  }
}
import { CommonModule } from "@angular/common"
import { MaterialModule } from "../../material/material.module"
