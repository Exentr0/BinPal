import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MainWindowComponentComponent } from './main-window-component.component';

describe('CreatePackageWindowComponentComponent', () => {
  let component: MainWindowComponentComponent;
  let fixture: ComponentFixture<MainWindowComponentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MainWindowComponentComponent]
    });
    fixture = TestBed.createComponent(MainWindowComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
