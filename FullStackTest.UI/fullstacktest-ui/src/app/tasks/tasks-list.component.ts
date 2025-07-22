import { Component, OnInit, inject, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NgModel } from '@angular/forms';
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
  errorMessage = '';

  @ViewChild('newTaskModel', { static: false }) newTaskModel!: NgModel;

  private tasksService = inject(TasksService);

  ngOnInit() {
    console.log('ngOnInit called');
    this.loadTasks();
  }

  loadTasks() {
    console.log('loadTasks called');
    
    this.clearError();

    this.tasksService.getTasks().subscribe({
      next: (tasks) => {
        console.log('Tasks received:', tasks);
        this.tasks = tasks;
      },
      error: (error) => {
        console.error('Error loading tasks:', error);
        this.handleError('Failed to load tasks. Please try again.', error);
      }
    });
  }

  addTask() {
    if (!this.newTaskName.trim()) 
      return;

    this.clearError();
    
    this.tasksService.createTask(this.newTaskName).subscribe({
      next: (response) => {
        console.log('Create task response:', response);
        
        // Use the response directly if it's a complete Task object
        // or construct it properly with fallback values
        const newTask: Task = {
          id: response.id || (response as any).Id || 0, // Handle different casing or missing ID
          name: response.name || this.newTaskName,
          completed: response.completed ?? false
        };
        
        // Validate that we have a proper ID
        if (newTask.id <= 0) {
          console.error('Invalid task ID received:', newTask.id);
          this.handleError('Task was created but received invalid ID. Refreshing task list...', response);
          // Reload tasks from server to get the correct data
          this.loadTasks();
          this.newTaskName = '';
          if (this.newTaskModel) {
            this.newTaskModel.reset();
          }
          return;
        }
        
        this.tasks.push(newTask);
        this.newTaskName = '';
        
        // Reset the form validation state
        if (this.newTaskModel) {
          this.newTaskModel.reset();
        }
      },
      error: (error) => {
        console.error('Error creating task:', error);
        this.handleError('Failed to create task. Please try again.', error);
      }
    });
  }

  toggleTask(myTask: Task) {
    console.log('Toggling task:', myTask);

    // Validate task has proper ID before making API call
    if (!myTask.id || myTask.id <= 0) {
      console.error('Cannot toggle task with invalid ID:', myTask);
      this.handleError('Task has invalid ID. Please refresh the page and try again.', myTask);
      return;
    }

    myTask.completed = !myTask.completed;
    
    this.clearError();

    this.tasksService.toggleTask(myTask).subscribe({
      next: () => {
        console.log(`Task ${myTask.id} toggled to ${myTask.completed}`);
      },
      error: (error) => {
        console.error('Error toggling task:', error);
        // Revert the change on error
        myTask.completed = !myTask.completed;
        this.handleError('Failed to update task. Please try again.', error);
      }
    });
  }

  clearError() {
    this.errorMessage = '';
  }

  handleError(userMessage: string, error: any) {
    this.errorMessage = userMessage;
    
    // You can enhance this to show more specific error messages
    if (error.error && error.error.message) {
      this.errorMessage += ` (${error.error.message})`;
    } else if (error.message) {
      this.errorMessage += ` (${error.message})`;
    }
  }
}
