import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './auth/register/register.component';
import { HomeComponent } from './home/home.component';
import { TaskAddComponent } from './task-add/task-add.component';
import { TaskEditComponent } from './task-edit/task-edit.component';
import { TaskHistoryEditComponent } from './task-history-edit/task-history-edit.component';

import { UserDetailsComponent } from './user-details/user-details.component';
import { UserUpdateComponent } from './user-update/user-update.component';

const routes: Routes = [
  { path: "", component: HomeComponent },
  { path: "register", component: RegisterComponent },
  { path: "user/:id", component: UserDetailsComponent },
  { path: "user/update/:id", component: UserUpdateComponent },
  { path: "task/add/:id", component: TaskAddComponent },
  { path: "task/edit/:id/:taskid", component: TaskEditComponent },
  { path: "taskHistory/edit/:id/:taskHistoryId", component: TaskHistoryEditComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
