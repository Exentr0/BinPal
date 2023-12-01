import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MediaFormComponentComponent } from './media-form-component.component';

describe('MediaFormComponentComponent', () => {
  let component: MediaFormComponentComponent;
  let fixture: ComponentFixture<MediaFormComponentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MediaFormComponentComponent]
    });
    fixture = TestBed.createComponent(MediaFormComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
