import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CenterSearchComponent } from './center-search.component';

describe('CenterSearchComponent', () => {
  let component: CenterSearchComponent;
  let fixture: ComponentFixture<CenterSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CenterSearchComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CenterSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
