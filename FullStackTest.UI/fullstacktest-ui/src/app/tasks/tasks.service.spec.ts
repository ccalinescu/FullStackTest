import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TasksService } from './tasks.service';
import { Task } from './task.model';

describe('TasksService', () => {
  let service: TasksService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [TasksService]
    });
    service = TestBed.inject(TasksService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should fetch tasks', () => {
    const mockTasks: Task[] = [
      { id: 1, name: 'Test Task 1', completed: false },
      { id: 2, name: 'Test Task 2', completed: true }
    ];

    service.getTasks().subscribe(tasks => {
      expect(tasks).toEqual(mockTasks);
    });

    const req = httpMock.expectOne('/api/MyTasks');
    expect(req.request.method).toBe('GET');
    req.flush(mockTasks);
  });

  it('should create a task', () => {
    const taskName = 'New Task';
    const mockTask: Task = { id: 3, name: taskName, completed: false };

    service.createTask(taskName).subscribe(task => {
      expect(task).toEqual(mockTask);
    });

    const req = httpMock.expectOne('/api/MyTasks');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual({ name: taskName });
    req.flush(mockTask);
  });

  it('should toggle a task', () => {
    const mockTask: Task = { id: 1, name: 'Test Task', completed: false };
    const updatedTask: Task = { ...mockTask, completed: true };

    service.toggleTask(updatedTask).subscribe(task => {
      expect(task).toEqual(updatedTask);
    });

    const req = httpMock.expectOne('/api/MyTasks');
    expect(req.request.method).toBe('PUT');
    expect(req.request.body).toEqual(updatedTask);
    req.flush(updatedTask);
  });
});
