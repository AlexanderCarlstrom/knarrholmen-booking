import React from 'react';
import { RouteProps, Redirect, Route } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

const PrivateRoute = (props: RouteProps) => {
  const { component: Component, ...rest } = props;
  const user = useAuth();

  return (
    <Route {...rest} render={(props) => (user !== null ? <Component {...props} /> : <Redirect to="/auth/login" />)} />
  );
};

export default PrivateRoute;
