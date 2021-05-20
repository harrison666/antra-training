import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TaskHistoryService } from '../core/services/task-history.service';
import { TaskHistoryRequest } from '../shared/model/taskHistoryRequest';

@Component({
  selector: 'app-task-history-edit',
  templateUrl: './task-history-edit.component.html',
  styleUrls: ['./task-history-edit.component.css']
})
export class TaskHistoryEditComponent implements OnInit {

  taskHistory: TaskHistoryRequest = {
    userId: 0, title:'', description:'', dueDate: new Date(), completed: new Date(), remarks:''
  }
  id!:number;
  constructor(private taskHistoryService: TaskHistoryService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    console.log("inside the init method");
    this.route.paramMap.subscribe((params) => {
      console.log(params);
      this.taskHistory.userId = +params.get('id')!;
      this.id = +params.get('taskHistoryId')!;
      this.getTaskHistory();
    });
  }

  getTaskHistory() {
    this.taskHistoryService.getTaskHistory(this.id).subscribe((response) => {
      this.taskHistory.title = response.title;
      this.taskHistory.description = response.description;
      this.taskHistory.dueDate = response.dueDate;
      this.taskHistory.completed = response.completed;
      this.taskHistory.remarks = response.remarks;
    });
  }

  editTaskHistory() {
    this.taskHistoryService.editTaskHistory(this.id, this.taskHistory).subscribe((response) => {
      if (response) {
        this.router.navigate(['/user/'+this.taskHistory.userId]);
      }})
  }
}
