import React, { useState } from 'react';
import { AxiosResponse } from 'axios';

import { ApiResponse, UserResponse } from '../types/ApiReponse';
import { ContainterProps } from '../types/ContainterProps';
import { privateFetch, publicFetch } from '../utils/axios';

const AuthContext = React.createContext(null);

const AuthProvider = ({ children }: ContainterProps) => {
  const [user, setUser] = useState(null);

  const logIn = (credentials: LoginValues) => {
    return publicFetch
      .post<UserResponse>('auth/login', credentials, { withCredentials: true })
      .then((response: AxiosResponse<UserResponse>) => {
        const { data } = response;
        if (data.success) {
          setUser(data.user);
        }
        return data;
      })
      .catch((err) => err);
  };

  const signUp = (credentials: SignUpValues) => publicFetch.post<ApiResponse>('auth/register', credentials);

  const loginWithToken = () => {
    return privateFetch
      .get<UserResponse>('auth/login-with-token')
      .then((res: AxiosResponse<UserResponse>) => {
        setUser(res.data.user);
      })
      .catch(() => {
        setUser(null);
      });
  };

  const logout = () => {
    publicFetch.post('auth/logout', { userId: user.id }, { withCredentials: true }).then(() => {
      setUser(null);
    });
  };

  return (
    <AuthContext.Provider
      value={{
        user,
        setUser,
        logIn: (credentials: LoginValues) => logIn(credentials),
        signUp: (credentials: SignUpValues) => signUp(credentials),
        loginWithToken: () => loginWithToken(),
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
