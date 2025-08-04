import { UserDto } from "./user.dto";

export interface UserList {
  data: UserDto[];
  pageNumber: number;
  pageSize: number;
  totalPages: number;
  totalRecords: number;
}