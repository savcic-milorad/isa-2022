import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DonorReservationOverviewComponent } from './donor-reservation-overview.component';

describe('DonorReservationOverviewComponent', () => {
  let component: DonorReservationOverviewComponent;
  let fixture: ComponentFixture<DonorReservationOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DonorReservationOverviewComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(DonorReservationOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
