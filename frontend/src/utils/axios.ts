import axios, { AxiosError, AxiosResponse } from 'axios';

import { useAuth } from '../context/AuthContext';

const publicFetch = axios.create({ baseURL: process.env.REACT_APP_API_URL });
const privateFetch = axios.create({ baseURL: process.env.REACT_APP_API_URL });

// logout when response status is 401 (unauthorized)
privateFetch.interceptors.response.use((response: AxiosResponse) => response),
  (error: AxiosError) => {
    if (error.response.status === 401) {
      AuthService.logout();
      return error;
    }
  };

export { publicFetch, privateFetch };
