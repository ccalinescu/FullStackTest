import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Task } from './task.model';

@Injectable({ providedIn: 'root' })
export class TasksService {
  private apiUrl = '/api/MyTasks';

  constructor(private _httpClient: HttpClient) {}

  getTasks(): Observable<Task[]> {
    return this._httpClient.get<Task[]>(this.apiUrl);
  }

  createTask(name: string): Observable<Task> {
    return this._httpClient.post<Task>(this.apiUrl, { name });
  }

  toggleTask(myTask: Task): Observable<Task> {
    return this._httpClient.put<Task>(`${this.apiUrl}`, myTask);
  }
}
