import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GridBookComponent } from './grid-book.component';

describe('GridBookComponent', () => {
  let component: GridBookComponent;
  let fixture: ComponentFixture<GridBookComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GridBookComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GridBookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
