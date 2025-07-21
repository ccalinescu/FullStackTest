import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Task } from './task.model';

@Injectable({ providedIn: 'root' })
export class TasksService {
  private apiUrl = '/api/MyTasks';

  constructor(private http: HttpClient) {}

  getTasks(): Observable<Task[]> {
    return this.http.get<Task[]>(this.apiUrl);
  }

  createTask(name: string): Observable<Task> {
    return this.http.post<Task>(this.apiUrl, { name });
  }

  toggleTask(task: Task): Observable<Task> {
    return this.http.put<Task>(`${this.apiUrl}`, task);
  }
}
