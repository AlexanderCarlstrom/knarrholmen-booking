import axios, { AxiosError, AxiosResponse } from 'axios';
import React from 'react';

import { ContainterProps } from '../types/ContainterProps';
import { publicFetch } from "../utils/axios";

const AxiosContext = React.createContext(null);

const AxiosProvider = ({ children }: ContainterProps) => {
  const publicFetch = axios.create({ baseURL: process.env.REACT_APP_API_URL });
  const privateFetch = axios.create({ baseURL: process.env.REACT_APP_API_URL });

// logout when response status is 401 (unauthorized)
  privateFetch.interceptors.response.use((response: AxiosResponse) => response),
    (error: AxiosError) => {
      if (error.response.status === 401) {
        console.log('logout');
        return error;
      }
    };

}
  return <AxiosContext.Provider value={ publicFetch, privateFetch }>{children}</AxiosContext.Provider>
};

const



export { publicFetch, privateFetch };
