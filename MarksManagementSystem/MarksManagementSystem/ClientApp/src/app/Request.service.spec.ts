import { TestBed } from '@angular/core/testing';
import { RequestService } from 'src/request.service';



describe('ClassServicesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RequestService = TestBed.get(RequestService);
    expect(service).toBeTruthy();
  });
});
