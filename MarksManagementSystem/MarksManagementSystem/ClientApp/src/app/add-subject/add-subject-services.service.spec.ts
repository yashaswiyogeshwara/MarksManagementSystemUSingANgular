import { TestBed } from '@angular/core/testing';

import { AddSubjectServicesService } from './add-subject-services.service';

describe('AddSubjectServicesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AddSubjectServicesService = TestBed.get(AddSubjectServicesService);
    expect(service).toBeTruthy();
  });
});
