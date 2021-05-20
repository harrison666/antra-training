import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Task } from 'src/app/shared/model/task';
import { TaskRequest } from 'src/app/shared/model/taskRequest';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  constructor(private apiService: ApiService) { }

  getTask(id: number): Observable<Task> {
    return this.apiService.getOne(`${'Task/'}`,id);
  }
  addTask(task: TaskRequest):Observable<Task> {
    return this.apiService.create(`${'Task/add'}`,task);
  }
  editTask(id: number, task: TaskRequest){
    return this.apiService.update(`${'Task/update/'}`+ id, task);
  }
  deleteTask(id: number) {
    return this.apiService.delete(`${'Task/delete/'}`, id);
  }
}
