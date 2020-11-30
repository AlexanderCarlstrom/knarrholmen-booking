import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter as Router } from 'react-router-dom';
import App from './App';
import { BreakpointProvider } from './context/BreakpointContext';
import './index.scss';
import reportWebVitals from './reportWebVitals';

// const breakpoints = {
//   mobile: 320,
//   mobileLandscape: 480,
//   tablet: 768,
//   tabletLandscape: 1024,
//   desktop: 1200,
//   desktopLarge: 1500,
//   desktopWide: 1920,
// };

ReactDOM.render(
  <React.StrictMode>
    <Router>
      <BreakpointProvider>
        <App />
      </BreakpointProvider>
    </Router>
  </React.StrictMode>,
  document.getElementById('root'),
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
