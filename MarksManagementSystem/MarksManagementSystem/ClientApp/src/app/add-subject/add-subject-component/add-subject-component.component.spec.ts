import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddSubjectComponentComponent } from './add-subject-component.component';

describe('AddSubjectComponentComponent', () => {
  let component: AddSubjectComponentComponent;
  let fixture: ComponentFixture<AddSubjectComponentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddSubjectComponentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddSubjectComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
