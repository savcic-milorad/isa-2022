import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DonorQuestionnaireComponent } from './donor-questionnaire.component';

describe('DonorQuestionnaireComponent', () => {
  let component: DonorQuestionnaireComponent;
  let fixture: ComponentFixture<DonorQuestionnaireComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DonorQuestionnaireComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(DonorQuestionnaireComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
