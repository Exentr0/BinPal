import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContentFormComponentComponent } from './content-form-component.component';

describe('ContentFormComponentComponent', () => {
  let component: ContentFormComponentComponent;
  let fixture: ComponentFixture<ContentFormComponentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ContentFormComponentComponent]
    });
    fixture = TestBed.createComponent(ContentFormComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
