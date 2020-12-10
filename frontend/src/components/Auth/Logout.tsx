import React, { useEffect, useState } from 'react';
import { Redirect } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';

const Logout = () => {
  const { logout } = useAuth();
  const [shouldRedirect, setShouldRedirect] = useState(false);
  let mounted = false;

  useEffect(() => {
    if (!mounted) {
      logout();
      setShouldRedirect(true);
    }
    mounted = true;
  });

  return shouldRedirect !== false ? <Redirect to="/" /> : null;
};

export default Logout;
