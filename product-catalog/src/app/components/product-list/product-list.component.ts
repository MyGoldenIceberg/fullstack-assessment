import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { CategoryService } from '../../services/category.service';
import { Product } from '../../models/product.model';
import { Category } from '../../models/category.model';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss'] 
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService
  ) {}

  ngOnInit(): void {
    forkJoin({
      products: this.productService.getProducts(),
      categories: this.categoryService.getCategories()
    }).subscribe(({ products, categories }) => {
      this.products = products.map(p => ({
        ...p,
        categoryName: categories.find(c => c.id == p.categoryId)?.name || 'Unknown'
      }));
    });
  }
}
