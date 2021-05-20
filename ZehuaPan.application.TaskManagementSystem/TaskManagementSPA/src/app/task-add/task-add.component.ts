import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TaskService } from '../core/services/task.service';
import { TaskRequest } from '../shared/model/taskRequest';

@Component({
  selector: 'app-task-add',
  templateUrl: './task-add.component.html',
  styleUrls: ['./task-add.component.css']
})
export class TaskAddComponent implements OnInit {

  task: TaskRequest = {
    userid: 0, title:'', description:'', dueDate: new Date(), priority:'', remarks:''
  }
  constructor(private taskService: TaskService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    console.log("inside the init method");
    this.route.paramMap.subscribe((params) => {
      console.log(params);
      this.task.userid = +params.get('id')!;
    });
  }

  addTask() {
    this.taskService.addTask(this.task).subscribe((response) => {
      if (response) {
        this.router.navigate(['/user/'+this.task.userid]);
      }})
  }

}
