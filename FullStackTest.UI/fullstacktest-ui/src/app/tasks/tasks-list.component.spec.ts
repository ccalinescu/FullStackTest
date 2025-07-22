import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { FormsModule } from '@angular/forms';
import { of, throwError } from 'rxjs';

import { TasksListComponent } from './tasks-list.component';
import { TasksService } from './tasks.service';
import { Task } from './task.model';

describe('TasksListComponent', () => {
  let component: TasksListComponent;
  let fixture: ComponentFixture<TasksListComponent>;
  let tasksService: jest.Mocked<TasksService>;

  const mockTasks: Task[] = [
    { id: 1, name: 'Test Task 1', completed: false },
    { id: 2, name: 'Test Task 2', completed: true }
  ];

  beforeEach(async () => {
    const tasksServiceSpy = {
      getTasks: jest.fn(),
      createTask: jest.fn(),
      toggleTask: jest.fn()
    };

    await TestBed.configureTestingModule({
      imports: [TasksListComponent, HttpClientTestingModule, FormsModule],
      providers: [
        { provide: TasksService, useValue: tasksServiceSpy }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(TasksListComponent);
    component = fixture.componentInstance;
    tasksService = TestBed.inject(TasksService) as jest.Mocked<TasksService>;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('ngOnInit', () => {
    it('should load tasks on init', () => {
      tasksService.getTasks.mockReturnValue(of(mockTasks));
      
      component.ngOnInit();
      
      expect(tasksService.getTasks).toHaveBeenCalled();
      expect(component.tasks).toEqual(mockTasks);
    });

    it('should handle error when loading tasks fails', () => {
      const error = { message: 'Network error' };
      tasksService.getTasks.mockReturnValue(throwError(() => error));
      
      component.ngOnInit();
      
      expect(component.errorMessage).toContain('Failed to load tasks');
    });
  });

  describe('addTask', () => {
    it('should add a new task successfully', () => {
      const newTask: Task = { id: 3, name: 'New Task', completed: false };
      component.newTaskName = 'New Task';
      tasksService.createTask.mockReturnValue(of(newTask));
      
      component.addTask();
      
      expect(tasksService.createTask).toHaveBeenCalledWith('New Task');
      expect(component.tasks.length).toBe(1);
      expect(component.tasks[0]).toEqual(newTask);
      expect(component.newTaskName).toBe('');
    });

    it('should not add task when name is empty', () => {
      component.newTaskName = '';
      
      component.addTask();
      
      expect(tasksService.createTask).not.toHaveBeenCalled();
    });

    it('should handle error when creating task fails', () => {
      const error = { message: 'Server error' };
      component.newTaskName = 'New Task';
      tasksService.createTask.mockReturnValue(throwError(() => error));
      
      component.addTask();
      
      expect(component.errorMessage).toContain('Failed to create task');
    });
  });

  describe('toggleTask', () => {
    beforeEach(() => {
      component.tasks = [...mockTasks];
    });

    it('should toggle task completion successfully', () => {
      const taskToToggle = { ...mockTasks[0] };
      tasksService.toggleTask.mockReturnValue(of(taskToToggle));
      
      component.toggleTask(taskToToggle);
      
      expect(taskToToggle.completed).toBe(true);
      expect(tasksService.toggleTask).toHaveBeenCalledWith(taskToToggle);
    });

    it('should revert toggle on error', () => {
      const taskToToggle = { ...mockTasks[0] };
      const originalCompleted = taskToToggle.completed;
      const error = { message: 'Update failed' };
      tasksService.toggleTask.mockReturnValue(throwError(() => error));
      
      component.toggleTask(taskToToggle);
      
      expect(taskToToggle.completed).toBe(originalCompleted);
      expect(component.errorMessage).toContain('Failed to update task');
    });

    it('should handle task with invalid ID', () => {
      const invalidTask: Task = { id: 0, name: 'Invalid Task', completed: false };
      
      component.toggleTask(invalidTask);
      
      expect(tasksService.toggleTask).not.toHaveBeenCalled();
      expect(component.errorMessage).toContain('invalid ID');
    });
  });

  describe('error handling', () => {
    it('should clear error message', () => {
      component.errorMessage = 'Some error';
      
      component.clearError();
      
      expect(component.errorMessage).toBe('');
    });

    it('should handle error with message', () => {
      const userMessage = 'Custom error';
      const error = { error: { message: 'Server error' } };
      
      component.handleError(userMessage, error);
      
      expect(component.errorMessage).toBe('Custom error (Server error)');
    });
  });
});
