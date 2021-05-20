import { Task } from "./task";
import { TaskHistory } from "./taskHistory";

export interface UserResponse {
    id: number;
    email: string;
    fullname: string;
    mobileno: string;

    tasks? : Task[];
    taskHistories? : TaskHistory[];
}