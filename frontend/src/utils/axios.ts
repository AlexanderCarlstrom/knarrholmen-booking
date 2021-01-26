import axios, { AxiosError, AxiosResponse } from 'axios';
import { UserResponse } from '../types/ApiReponse';

const publicFetch = axios.create({ baseURL: process.env.REACT_APP_API_URL });
const privateFetch = axios.create({ baseURL: process.env.REACT_APP_API_URL, withCredentials: true });

const setUpAuthInterceptors = (refreshToken: () => Promise<AxiosResponse<UserResponse>>, logout: () => void) => {
  privateFetch.interceptors.response.use(
    (response) => response,
    (error) => {
      const originalRequest = error.config;
      if (error.response.status === 401) {
        refreshToken()
          .then(() => {
            return axios(originalRequest);
          })
          .catch(() => {
            logout();
            return Promise.reject();
          });
      }
    },
  );
};

export { publicFetch, privateFetch, setUpAuthInterceptors };
