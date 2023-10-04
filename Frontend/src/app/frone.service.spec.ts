import { TestBed } from '@angular/core/testing';

import { FroneService } from './frone.service';

describe('FroneService', () => {
  let service: FroneService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FroneService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
