export interface Task {
    id: number;
    userid: number;
    title: string;
    description: string;
    dueDate: Date;
    priority: string;
    remarks: string;
    user?: any;
}