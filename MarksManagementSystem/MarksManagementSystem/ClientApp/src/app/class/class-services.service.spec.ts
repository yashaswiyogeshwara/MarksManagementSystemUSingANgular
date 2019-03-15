import { TestBed } from '@angular/core/testing';

import { ClassServicesService } from './class-services.service';

describe('ClassServicesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ClassServicesService = TestBed.get(ClassServicesService);
    expect(service).toBeTruthy();
  });
});
