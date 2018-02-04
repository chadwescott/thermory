import { TestBed, inject } from '@angular/core/testing';

import { LumberCategoryService } from './lumber-category.service';

describe('LumberCategoryService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LumberCategoryService]
    });
  });

  it('should be created', inject([LumberCategoryService], (service: LumberCategoryService) => {
    expect(service).toBeTruthy();
  }));
});
