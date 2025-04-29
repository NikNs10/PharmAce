import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageDrugsComponent } from './manage-drugs.component';

describe('ManageDrugsComponent', () => {
  let component: ManageDrugsComponent;
  let fixture: ComponentFixture<ManageDrugsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ManageDrugsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageDrugsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
