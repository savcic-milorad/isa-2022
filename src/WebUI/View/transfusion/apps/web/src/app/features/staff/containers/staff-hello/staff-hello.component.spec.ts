import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StaffHelloComponent } from './staff-hello.component';

describe('StaffHelloComponent', () => {
  let component: StaffHelloComponent;
  let fixture: ComponentFixture<StaffHelloComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [StaffHelloComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(StaffHelloComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
