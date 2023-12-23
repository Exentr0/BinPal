import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoriesFormComponentComponent } from './categories-form-component.component';

describe('CategoiresFormComponentComponent', () => {
  let component: CategoriesFormComponentComponent;
  let fixture: ComponentFixture<CategoriesFormComponentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CategoriesFormComponentComponent]
    });
    fixture = TestBed.createComponent(CategoriesFormComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
