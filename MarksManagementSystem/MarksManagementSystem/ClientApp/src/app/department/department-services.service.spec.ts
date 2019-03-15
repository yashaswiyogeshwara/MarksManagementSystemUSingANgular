import { TestBed } from '@angular/core/testing';

import { DepartmentServicesService } from './department-services.service';

describe('DepartmentServicesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DepartmentServicesService = TestBed.get(DepartmentServicesService);
    expect(service).toBeTruthy();
  });
});
