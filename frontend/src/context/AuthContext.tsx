import React, { useState } from 'react';
import { AxiosResponse } from 'axios';

import { ContainterProps } from '../types/ContainterProps';
import { ApiResponse } from '../types/ApiReponse';
import { publicFetch } from '../utils/axios';

const AuthContext = React.createContext(null);

const AuthProvider = ({ children }: ContainterProps) => {
  const [user, setUser] = useState(null);

  const login = (credentials: LoginValues) => {
    return publicFetch
      .post<ApiResponse>('users/login', credentials)
      .then((response: AxiosResponse<ApiResponse>) => {
        const { data } = response;
        if (data.success) {
          setUser(user);
        }
        return data;
      })
      .catch((err) => err.response.data);
  };

  const signUp = (credentials: SignUpValues) => publicFetch.post<ApiResponse>('users/register', credentials);

  return (
    <AuthContext.Provider
      value={{
        user,
        setUser,
        login: (credentials: LoginValues) => login(credentials),
        signUp: (credentials: SignUpValues) => signUp(credentials),
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};

const useAuth = () => React.useContext(AuthContext);

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

export { AuthProvider, useAuth };
