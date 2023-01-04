import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TermManagementComponent } from './term-management.component';

describe('TermManagementComponent', () => {
  let component: TermManagementComponent;
  let fixture: ComponentFixture<TermManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TermManagementComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(TermManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
