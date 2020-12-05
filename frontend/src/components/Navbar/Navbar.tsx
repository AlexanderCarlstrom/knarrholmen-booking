import { Link, NavLink, RouteComponentProps, withRouter } from 'react-router-dom';
import { Button, Drawer, Layout, Menu } from 'antd';
import { MenuOutlined } from '@ant-design/icons';
import React, { useState } from 'react';
import 'antd/dist/antd.css';

import { useBreakpoint } from '../../context/BreakpointContext';
import AuthService from '../../services/auth.service';
import './Navbar.scss';

const { Header } = Layout;

const Navbar = (props: RouteComponentProps) => {
  const width = useBreakpoint();
  const breakpoint = 1024;
  return (
    <React.Fragment>
      {width < breakpoint ? (
        <MobileMenu pathName={props.location.pathname} />
      ) : (
        <DesktopMenu pathName={props.location.pathname} />
      )}
    </React.Fragment>
  );
};

const DesktopMenu = ({ pathName }: MenuProps) => {
  return (
    <React.Fragment>
      <Header className="navbar">
        <NavLink to="/" className="brand">
          Knarrholmen
        </NavLink>

        <div className="navigation">
          <Menu selectedKeys={[pathName]} mode="horizontal">
            <Menu.Item key="/">
              <Link to="/">Home</Link>
            </Menu.Item>
            <Menu.Item key="/activities">
              <Link to="/activities">Activities</Link>
            </Menu.Item>
          </Menu>
          <div className="buttons">
            <Button type="primary" ghost className="button">
              <Link to="/auth/sign-in">Sign In</Link>
            </Button>
            <Button type="primary" className="button">
              <Link to="/auth/sign-up">Sign Up</Link>
            </Button>
          </div>
        </div>
      </Header>
    </React.Fragment>
  );
};

const MobileMenu = ({ pathName }: MenuProps) => {
  const [visible, setVisible] = useState(false);

  const showDrawer = () => setVisible(true);
  const closeDrawer = () => setVisible(false);

  return (
    <React.Fragment>
      <Header className="navbar mobile">
        <NavLink to="/" className="brand">
          Booking
        </NavLink>

        <Button type="primary" ghost icon={<MenuOutlined />} onClick={showDrawer} />
      </Header>
      <Drawer title="Menu" width="300" placement="right" onClose={closeDrawer} visible={visible} className="drawer">
        <Menu selectedKeys={[pathName]} mode="inline" className="drawer-menu">
          <Menu.Item key="/">
            <Link to="/">Home</Link>
          </Menu.Item>
          <Menu.Item key="/activities">
            <Link to="/activities">Activities</Link>
          </Menu.Item>
        </Menu>
        {AuthService.user === null && (
          <Menu mode="inline" className="drawer-menu">
            <Menu.Item>
              <Link to="/auth/sign-in">Sign In</Link>
            </Menu.Item>
            <Menu.Item>
              <Link to="/auth/sign-up">Sign Up</Link>
            </Menu.Item>
          </Menu>
        )}
      </Drawer>
    </React.Fragment>
  );
};

type MenuProps = { pathName: string };

export default withRouter(Navbar);
