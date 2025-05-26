export interface Drug {
  drugId: string;
  name: string;
  description: string;
  price: number;
  imageUrl?: string;
  categoryId: string;
  //manufacturer: string;
  stock: number;
  //discount?: number;
  //createdAt: Date;
}