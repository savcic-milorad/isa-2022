import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministratorHelloComponent } from './administrator-hello.component';

describe('AdministratorHelloComponent', () => {
  let component: AdministratorHelloComponent;
  let fixture: ComponentFixture<AdministratorHelloComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AdministratorHelloComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(AdministratorHelloComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
