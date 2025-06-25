export interface Product {
  id: number;
  name: string;
  description: string;
  price: number;
  categoryId: string; 
  categoryName?: string; // Used for UI only
}
