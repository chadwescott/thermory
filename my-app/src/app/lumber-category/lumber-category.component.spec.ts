import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LumberCategoryComponent } from './lumber-category.component';

describe('LumberCategoryComponent', () => {
  let component: LumberCategoryComponent;
  let fixture: ComponentFixture<LumberCategoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LumberCategoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LumberCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
