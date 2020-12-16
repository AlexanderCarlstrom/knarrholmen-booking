import React, { useState } from 'react';
import { AxiosResponse } from 'axios';

import { ContainterProps } from '../types/ContainterProps';
import { ApiResponse } from '../types/ApiReponse';
import { publicFetch } from '../utils/axios';

const AuthContext = React.createContext(null);

const AuthProvider = ({ children }: ContainterProps) => {
  const [user, setUser] = useState(null);

  const logIn = (credentials: LoginValues) => {
    return publicFetch
      .post<ApiResponse>('auth/login', credentials)
      .then((response: AxiosResponse<ApiResponse>) => {
        const { data } = response;
        if (data.success) {
          setUser(data.user);
        }
        return data;
      })
      .catch((err) => err);
  };

  const signUp = (credentials: SignUpValues) => publicFetch.post<ApiResponse>('auth/register', credentials);

  const logout = () => {
    setUser(null);
  };

  return (
    <AuthContext.Provider
      value={{
        user,
        setUser,
        logIn: (credentials: LoginValues) => logIn(credentials),
        signUp: (credentials: SignUpValues) => signUp(credentials),
        logout: () => logout(),
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
