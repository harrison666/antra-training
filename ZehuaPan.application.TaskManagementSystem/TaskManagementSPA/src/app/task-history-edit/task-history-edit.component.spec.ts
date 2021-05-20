import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskHistoryEditComponent } from './task-history-edit.component';

describe('TaskHistoryEditComponent', () => {
  let component: TaskHistoryEditComponent;
  let fixture: ComponentFixture<TaskHistoryEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskHistoryEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskHistoryEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
