import { User } from "./user.model";

export interface GetUsersResponse {
    pageIndex: number;
    pageSize: number;
    totalCount: number;
    data: User[];
}