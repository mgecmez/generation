import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditPowerplantComponent } from './add-edit-powerplant.component';

describe('AddEditPowerplantComponent', () => {
  let component: AddEditPowerplantComponent;
  let fixture: ComponentFixture<AddEditPowerplantComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEditPowerplantComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditPowerplantComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
