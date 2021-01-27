import { Dispatch } from 'react';
import axios from 'axios';

import { User } from '../types/User';

const publicFetch = axios.create({ baseURL: process.env.REACT_APP_API_URL });
const privateFetch = axios.create({ baseURL: process.env.REACT_APP_API_URL, withCredentials: true });

const setUpAuthInterceptors = (setUser: Dispatch<User>, navigateTo: (path: string) => void) => {
  privateFetch.interceptors.response.use(
    (response) => response,
    (error) => {
      const originalRequest = error.config;
      if (error.response.status === 401) {
        publicFetch
          .get('auth/refresh-token', { withCredentials: true })
          .then(() => {
            return axios(originalRequest);
          })
          .catch(() => {
            setUser(null);
            navigateTo('/auth/login');
            return Promise.reject();
          });
      }
    },
  );
};

export { publicFetch, privateFetch, setUpAuthInterceptors };
