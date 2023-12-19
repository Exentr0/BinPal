import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GeneralInfoFormComponentComponent } from './general-info-form-component.component';

describe('PackageGeneralFormComponentComponent', () => {
  let component: GeneralInfoFormComponentComponent;
  let fixture: ComponentFixture<GeneralInfoFormComponentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GeneralInfoFormComponentComponent]
    });
    fixture = TestBed.createComponent(GeneralInfoFormComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
