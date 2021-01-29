import { User } from './User';
import { ActivityListItem, Activity } from './Activity';

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
  activities?: ActivityListItem[];
  activity?: Activity;
}
