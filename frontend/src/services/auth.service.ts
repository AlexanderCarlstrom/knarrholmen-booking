import { AxiosResponse } from 'axios';

import { ApiResponse, UserResponse } from '../types/ApiReponse';
import { publicFetch } from '../utils/axios';
import { User } from '../types/User';

class AuthService {
  private _user: User;
  public localStorageKey = 'user-access-token';

  get user() {
    return this._user;
  }

  login(credentials: LoginValues) {
    return publicFetch
      .post<UserResponse>('auth/login', credentials)
      .then((response: AxiosResponse<UserResponse>) => {
        const { data } = response;
        if (data.success) {
          localStorage.setItem(this.localStorageKey, data.jwt);
          this._user = data.user;
        }
        return data;
      })
      .catch((err) => err.response.data);
  }

  signUp(credentials: SignUpValues) {
    return publicFetch.post<ApiResponse>('auth/register', credentials);
  }

  logout() {
    this._user = null;
    this.privateFetch;
  }
}

type LoginValues = {
  email: string;
  password: string;
  remember: boolean;
};

type SignUpValues = {
  firstName: string;
  lastName?: string;
  email: string;
  password: string;
};

export default new AuthService();
