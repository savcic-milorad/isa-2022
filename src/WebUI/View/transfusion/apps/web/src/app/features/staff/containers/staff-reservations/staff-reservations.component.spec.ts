import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StaffReservationsComponent } from './staff-reservations.component';

describe('StaffReservationsComponent', () => {
  let component: StaffReservationsComponent;
  let fixture: ComponentFixture<StaffReservationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [StaffReservationsComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(StaffReservationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
