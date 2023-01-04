import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DonorReservationSearchComponent } from './donor-reservation-search.component';

describe('DonorReservationSearchComponent', () => {
  let component: DonorReservationSearchComponent;
  let fixture: ComponentFixture<DonorReservationSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DonorReservationSearchComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(DonorReservationSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
