import { Dispatch } from 'react';
import axios, { AxiosResponse } from 'axios';

import { User } from '../types/User';
import { UserResponse } from '../types/ApiReponse';

const publicFetch = axios.create({ baseURL: process.env.REACT_APP_API_URL });
const privateFetch = axios.create({ baseURL: process.env.REACT_APP_API_URL, withCredentials: true });

const setUpAuthInterceptors = (setUser: Dispatch<User>, loginWithToken: () => AxiosResponse<UserResponse>) => {
  privateFetch.interceptors.response.use(
    (response) => response,
    (error) => {
      if (error.response.status === 401) {
        publicFetch
          .get('auth/refresh-token', { withCredentials: true })
          .then(() => {
            return loginWithToken();
          })
          .catch(() => {
            setUser(null);
            return Promise.reject();
          });
      }
    },
  );
};

export { publicFetch, privateFetch, setUpAuthInterceptors };
