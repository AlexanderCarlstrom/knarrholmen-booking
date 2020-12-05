import { BrowserRouter } from 'react-router-dom';
import React from 'react';

import { BreakpointProvider } from './context/BreakpointContext';
import { AuthProvider } from './context/AuthContext';
import { ContainterProps } from './types/ContainterProps';

const AppProvider = ({ children }: ContainterProps) => {
  return (
    <BrowserRouter>
      <BreakpointProvider>
        <AuthProvider>{children}</AuthProvider>
      </BreakpointProvider>
    </BrowserRouter>
  );
};

export default AppProvider;
