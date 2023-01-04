import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministratorHomepageComponent } from './administrator-homepage.component';

describe('AdministratorHomepageComponent', () => {
  let component: AdministratorHomepageComponent;
  let fixture: ComponentFixture<AdministratorHomepageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AdministratorHomepageComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(AdministratorHomepageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
