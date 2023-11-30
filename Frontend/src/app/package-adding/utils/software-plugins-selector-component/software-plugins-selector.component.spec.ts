import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SoftwarePluginsSelectorComponent } from './software-plugins-selector.component';

describe('SoftwarePluginsMultieSelectComponentComponent', () => {
  let component: SoftwarePluginsSelectorComponent;
  let fixture: ComponentFixture<SoftwarePluginsSelectorComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SoftwarePluginsSelectorComponent]
    });
    fixture = TestBed.createComponent(SoftwarePluginsSelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
