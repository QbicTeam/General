/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ComercialService } from './comercial.service';

describe('Service: Comercial', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ComercialService]
    });
  });

  it('should ...', inject([ComercialService], (service: ComercialService) => {
    expect(service).toBeTruthy();
  }));
});
