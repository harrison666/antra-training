import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TaskHistoryService } from '../core/services/task-history.service';
import { TaskService } from '../core/services/task.service';
import { UserService } from '../core/services/user.service';
import { Task } from '../shared/model/task';
import { TaskHistory } from '../shared/model/taskHistory';
import { TaskHistoryRequest } from '../shared/model/taskHistoryRequest';
import { UserResponse } from '../shared/model/userReponse';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {

  user: UserResponse | undefined;
  id!: number;
  tasks!: Task[];
  taskHistories: TaskHistory[] | undefined;
  constructor(
    private userService: UserService,
    private taskService: TaskService,
    private taskHistoryService: TaskHistoryService,
    private route: ActivatedRoute,
    private router: Router
    ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      console.log(params);
      this.id = +params.get('id')!;
      this.fetchData();
    });
  }
  fetchData() {
    this.userService.getUserById(this.id).subscribe(u => {
      this.user = u;
    });
  }

  deleteTask(id: number) {
    console.log("inside deleteTask");
    this.taskService.deleteTask(id).subscribe(() => {
      console.log("Task deleted");
      this.fetchData();
    });
  
  }

  finishTask(task: Task) {
    this.deleteTask(task.id);
    let taskHistory:TaskHistoryRequest = {
      userId: task.userid,
      title: task.title, 
      description: task.title, 
      dueDate:task.dueDate, 
      completed: new Date(), 
      remarks: task.remarks
    };
    this.taskHistoryService.createTaskHistory(taskHistory).subscribe((response) => {
      console.log("History Created!");
      this.fetchData();
    });
  }

  deleteTaskHistory(id: number) {
    this.taskHistoryService.deleteTaskHistory(id).subscribe(() => {
      console.log("TaskHistory deleted");
      this.fetchData();
    });
  }


}
