import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';
import { LumberCategory } from './interfaces/lumber-category';

@Injectable()
export class LumberCategoryService {
  constructor(private http: Http) { }

  getLumberCategories() {
      const data: LumberCategory[] = [{
          name: 'Category 2',
          id: '2',
          sortOrder: 2
      },
      {
          name: 'Category 2',
          id: '2',
          sortOrder: 2
      }];
      return data;
    }
}
