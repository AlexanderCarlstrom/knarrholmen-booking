import { Link, NavLink, RouteComponentProps, withRouter } from 'react-router-dom';
import { Button, Drawer, Dropdown, Layout, Menu } from 'antd';
import { DownOutlined, MenuOutlined } from '@ant-design/icons';
import React, { useState } from 'react';
import 'antd/dist/antd.css';

import { useBreakpoint } from '../../context/BreakpointContext';
import { useAuth } from '../../context/AuthContext';
import './Navbar.scss';
import { User } from '../../types/User';

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
  const { user } = useAuth();
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
          {user === null ? (
            <div className="buttons">
              <Button type="primary" ghost className="button">
                <Link to="/auth/login">Log In</Link>
              </Button>
              <Button type="primary" className="button">
                <Link to="/auth/sign-up">Sign Up</Link>
              </Button>
            </div>
          ) : (
            <UserDropdown user={user} />
          )}
        </div>
      </Header>
    </React.Fragment>
  );
};

const MobileMenu = ({ pathName }: MenuProps) => {
  const [visible, setVisible] = useState(false);
  const { user } = useAuth();

  const showDrawer = () => setVisible(true);
  const closeDrawer = () => setVisible(false);

  return (
    <React.Fragment>
      <Header className="navbar mobile">
        <NavLink to="/" className="brand">
          Booking
        </NavLink>
        <Button type="primary" ghost icon={<MenuOutlined />} onClick={showDrawer} className="drawer-btn" />
        {user !== null && <UserDropdown user={user} />}
      </Header>
      <Drawer title="Menu" width="300" placement="left" onClose={closeDrawer} visible={visible} className="drawer">
        <Menu selectedKeys={[pathName]} mode="inline" className="drawer-menu">
          <Menu.Item key="/">
            <Link to="/">Home</Link>
          </Menu.Item>
          <Menu.Item key="/activities">
            <Link to="/activities">Activities</Link>
          </Menu.Item>
        </Menu>
        {user === null && (
          <Menu mode="inline" className="drawer-menu">
            <Menu.Item>
              <Link to="/auth/login">Log In</Link>
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

const UserDropdown = ({ user }: UserDropdownProps) => {
  const dropdownUserMenu = (
    <Menu>
      <Menu.Item>
        <Link to="/profile">Profile</Link>
      </Menu.Item>
      <Menu.Item>
        <Link to="/logout">Logout</Link>
      </Menu.Item>
    </Menu>
  );

  return (
    <Dropdown overlay={dropdownUserMenu} arrow trigger={['hover', 'click']} placement="bottomRight">
      <Button className="user-btn" type="primary" ghost>
        {createUserDisplayName(user.firstName)}
        <DownOutlined />
      </Button>
    </Dropdown>
  );
};

const createUserDisplayName = (firstName: string) => {
  if (firstName.length < 15) return firstName;
  return firstName.substr(0, 10) + '...';
};

type MenuProps = { pathName: string };

type UserDropdownProps = { user: User };

export default withRouter(Navbar);
