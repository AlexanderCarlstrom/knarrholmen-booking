import { Layout } from 'antd';
import 'antd/dist/antd.css';
import React from 'react';
import { Redirect, Route, RouteComponentProps, Switch, withRouter } from 'react-router-dom';
import './App.scss';
import Activities from './components/Activities/Activities';
import Auth from './components/Auth/Auth';
import Footer from './components/Footer/Footer';
import Home from './components/Home/Home';
import Navbar from './components/Navbar/Navbar';

const { Content } = Layout;

const App = (props: RouteComponentProps) => {
  return (
    <Layout className="app">
      {props.location.pathname !== '/auth/sign-in' && props.location.pathname !== '/auth/sign-up' ? <Navbar /> : null}
      <Content className="content">
        <Switch>
          <Route exact path="/" component={Home} />
          <Route path="/activities" component={Activities} />
          <Route path="/auth" component={Auth} />
          <Route render={() => <Redirect exact to="/" />} />
        </Switch>
      </Content>
      {props.location.pathname !== '/auth/sign-in' && props.location.pathname !== '/auth/sign-up' ? <Footer /> : null}
    </Layout>
  );
};

export default withRouter(App);
