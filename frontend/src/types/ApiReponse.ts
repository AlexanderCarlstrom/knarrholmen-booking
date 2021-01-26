import { User } from './User';
import { Activities, Activity } from './Activities';

export interface ApiResponse {
  success: boolean;
  status: number;
  message: string;
  errors?: string[];
}

export interface UserResponse extends ApiResponse {
  user: User;
}

export interface ActivitiesResponse {
  activities?: Activities[];
  activity?: Activity;
}
