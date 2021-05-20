import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TaskHistory } from 'src/app/shared/model/taskHistory';
import { TaskHistoryRequest } from 'src/app/shared/model/taskHistoryRequest';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class TaskHistoryService {

  constructor(private apiService: ApiService) { }

  getTaskHistory(id: number): Observable<TaskHistory> {
    return this.apiService.getOne(`${'TaskHistory/'}`,id);
  }
  createTaskHistory(taskHistory: TaskHistoryRequest): Observable<TaskHistory> {
    return this.apiService.create(`${'TaskHistory/add'}`, taskHistory);
  }
  editTaskHistory(id: number, task: TaskHistoryRequest){
    return this.apiService.update(`${'TaskHistory/update/'}`+ id, task);
  }
  deleteTaskHistory(id: number) {
    return this.apiService.delete(`${'TaskHistory/delete/'}`, id);
  }

}
