import { User } from './User';

export interface ApiResponse {
  success: boolean;
  status: number;
  message: string;
  errors?: string[];
  user?: User;
}
