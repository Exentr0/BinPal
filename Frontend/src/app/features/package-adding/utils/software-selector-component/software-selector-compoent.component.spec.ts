import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SoftwareSelectorComponent } from './software-selector-compoent.component';

describe('SoftwareSelectorCompoentComponent', () => {
  let component: SoftwareSelectorComponent;
  let fixture: ComponentFixture<SoftwareSelectorComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SoftwareSelectorComponent]
    });
    fixture = TestBed.createComponent(SoftwareSelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
