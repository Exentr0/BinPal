import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RequiredPluginsFormComponent } from './required-plugins-form.component';

describe('RequiredPluginsFormComponentComponent', () => {
  let component: RequiredPluginsFormComponent;
  let fixture: ComponentFixture<RequiredPluginsFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RequiredPluginsFormComponent]
    });
    fixture = TestBed.createComponent(RequiredPluginsFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
