export interface TaskHistoryRequest {
    userId: number;
    title: string;
    description: string;
    dueDate: Date;
    completed: Date;
    remarks: string;
}