import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddOrUpdateBookComponent } from './add-or-update-book.component';

describe('AddOrUpdateBookComponent', () => {
  let component: AddOrUpdateBookComponent;
  let fixture: ComponentFixture<AddOrUpdateBookComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddOrUpdateBookComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddOrUpdateBookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
