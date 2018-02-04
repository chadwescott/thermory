import { Component, Input, OnInit } from '@angular/core';
import { LumberCategory } from '../interfaces/lumber-category';

@Component({
  selector: 'app-lumber-category',
  templateUrl: './lumber-category.component.html',
  styleUrls: ['./lumber-category.component.css']
})
export class LumberCategoryComponent implements OnInit {
  @Input() data: LumberCategory;

  constructor() { }

  ngOnInit() {
  }

    showCategoryForm() {
        console.log('showCategoryForm');
    }

}
