import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SupportedSoftwareFormComponent } from './supported-software-form-component.component';

describe('PackageSupportedAppsFormComponentComponent', () => {
  let component: SupportedSoftwareFormComponent;
  let fixture: ComponentFixture<SupportedSoftwareFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SupportedSoftwareFormComponent]
    });
    fixture = TestBed.createComponent(SupportedSoftwareFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
