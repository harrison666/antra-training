import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TaskService } from '../core/services/task.service';
import { TaskRequest } from '../shared/model/taskRequest';

@Component({
  selector: 'app-task-edit',
  templateUrl: './task-edit.component.html',
  styleUrls: ['./task-edit.component.css']
})
export class TaskEditComponent implements OnInit {

  task: TaskRequest = {
    userid: 0, title:'', description:'', dueDate: new Date(), priority:'', remarks:''
  }
  id!:number;
  constructor(private taskService: TaskService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    console.log("inside the init method");
    this.route.paramMap.subscribe((params) => {
      console.log(params);
      this.task.userid = +params.get('id')!;
      this.id = +params.get('taskid')!;
      this.getTask();
    });
  }

  getTask() {
    this.taskService.getTask(this.id).subscribe((response) => {
      this.task.title = response.title;
      this.task.description = response.description;
      this.task.dueDate = response.dueDate;
      this.task.priority = response.priority;
      this.task.remarks = response.remarks;
    });
  }

  editTask() {
    this.taskService.editTask(this.id, this.task).subscribe((response) => {
      if (response) {
        this.router.navigate(['/user/'+this.task.userid]);
      }})
  }

}
