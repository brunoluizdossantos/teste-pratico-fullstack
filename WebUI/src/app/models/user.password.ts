import { UserDto } from "./user.dto";

export interface UserPassword extends UserDto {
  password: string;
}