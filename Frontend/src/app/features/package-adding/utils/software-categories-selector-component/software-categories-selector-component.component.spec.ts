import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SoftwareCategoriesSelectorComponentComponent } from './software-categories-selector-component.component';

describe('SoftwareCategoriesSelectorComponentComponent', () => {
  let component: SoftwareCategoriesSelectorComponentComponent;
  let fixture: ComponentFixture<SoftwareCategoriesSelectorComponentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SoftwareCategoriesSelectorComponentComponent]
    });
    fixture = TestBed.createComponent(SoftwareCategoriesSelectorComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
