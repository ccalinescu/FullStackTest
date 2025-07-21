import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TasksService } from './tasks.service';
import { Task } from './task.model';

@Component({
  selector: 'app-tasks-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './tasks-list.component.html',
  styleUrls: ['./tasks-list.component.css']
})
export class TasksListComponent implements OnInit {
  tasks: Task[] = [];
  newTaskName = '';

  private tasksService = inject(TasksService);

  ngOnInit() {
    console.log('ngOnInit called');
    this.loadTasks();
  }

  loadTasks() {
    console.log('loadTasks called');
    this.tasksService.getTasks().subscribe({
      next: (tasks) => {
        console.log('Tasks received:', tasks);
        this.tasks = tasks;
      },
      error: (error) => {
        console.error('Error loading tasks:', error);
      }
    });
  }

  addTask() {
    if (!this.newTaskName.trim()) return;
    this.tasksService.createTask(this.newTaskName).subscribe(response => {
      const newTask: Task = {
        id: response.id,
        name: this.newTaskName,
        completed: response.completed
      };
      this.tasks.push(newTask);
      this.newTaskName = '';
    });
  }

  toggleTask(task: Task) {
    task.completed = !task.completed;
    this.tasksService.toggleTask(task).subscribe();
    //this.loadTasks()
  }
}
