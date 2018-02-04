import { Component } from '@angular/core';
import { LumberCategoryComponent } from './lumber-category/lumber-category.component';
import { LumberCategory } from './interfaces/lumber-category';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Chad\'s App';
  lumberCategory: LumberCategory;

  constructor() {
    this.lumberCategory = {
      name: 'Category 1',
      id: '1',
      sortOrder: 1
    };
  }
}
