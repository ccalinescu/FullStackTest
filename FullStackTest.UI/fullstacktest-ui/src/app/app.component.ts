import { Component } from '@angular/core';
import { TasksListComponent } from './tasks/tasks-list.component';

@Component({
  selector: 'app-root',
  imports: [TasksListComponent],
  template: `
    <h1>Welcome to {{title}}!</h1>
    <app-tasks-list></app-tasks-list>
  `,
  styles: [],
})

export class AppComponent {
  title = 'Tasks Management Application';
}
