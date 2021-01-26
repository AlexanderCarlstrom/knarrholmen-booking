import { Link, NavLink, RouteComponentProps, withRouter } from 'react-router-dom';
import { Button, Drawer, Dropdown, Layout, Menu } from 'antd';
import {
  DownOutlined,
  MenuOutlined,
  HomeOutlined,
  CalendarOutlined,
  UserOutlined,
  LogoutOutlined,
  RocketOutlined,
  LoginOutlined,
  FormOutlined,
} from '@ant-design/icons';
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
        <MobileMenu pathName={props.location.pathname} width={width} />
      ) : (
        <DesktopMenu pathName={props.location.pathname} width={width} />
      )}
    </React.Fragment>
  );
};

const DesktopMenu = ({ pathName }: MenuProps) => {
  const { user } = useAuth();
  return (
    <React.Fragment>
      <Header className="navbar">
        <div className="navbar-container">
          <NavLink to="/" className="brand">
            KNARRHOLMEN
          </NavLink>

          <div className="navigation">
            <Menu selectedKeys={[pathName]} mode="horizontal" className="menu">
              <Menu.Item key="/" className="menu-item">
                <Link to="/">Home</Link>
              </Menu.Item>
              <Menu.Item key="/activities" className="menu-item">
                <Link to="/activities">Activities</Link>
              </Menu.Item>
              {user === null && (
                <>
                  <Menu.Item className="menu-item">
                    <Link to="/auth/login">Log In</Link>
                  </Menu.Item>
                  <Menu.Item className="menu-item">
                    <Link to="/auth/sign-up">Sign Up</Link>
                  </Menu.Item>
                </>
              )}
            </Menu>
            {user !== null && <UserDropdown user={user} />}
          </div>
        </div>
      </Header>
    </React.Fragment>
  );
};

const MobileMenu = ({ pathName, width }: MenuProps) => {
  const [visible, setVisible] = useState(false);
  const { user } = useAuth();

  const showDrawer = () => setVisible(true);
  const closeDrawer = () => setVisible(false);

  return (
    <React.Fragment>
      <Header className="navbar">
        <div className="navbar-container">
          <a href="" className="brand">
            KNARRHOLMEN
          </a>
          <div className="navigation">
            {width >= 768 && user !== null && <UserDropdown user={user} />}
            <Button ghost icon={<MenuOutlined />} onClick={showDrawer} className="drawer-btn" />
          </div>
        </div>
      </Header>
      <Drawer title="Menu" width="300" placement="left" onClose={closeDrawer} visible={visible} className="drawer">
        <Menu selectedKeys={[pathName]} mode="inline" className="drawer-menu" theme="light">
          <Menu.Item key="/" icon={<HomeOutlined />} className="menu-item">
            <Link to="/">Home</Link>
          </Menu.Item>
          <Menu.Item key="/activities" icon={<RocketOutlined />} className="menu-item">
            <Link to="/activities">Activities</Link>
          </Menu.Item>

          {width < 768 && user !== null && (
            <Menu.ItemGroup title={createUserDisplayName(user.firstName)} className="user-name">
              <Menu.Item icon={<UserOutlined />}>
                <Link to="/profile">Profile</Link>
              </Menu.Item>
              <Menu.Item icon={<CalendarOutlined />}>
                <Link to="/bookings">Bookings</Link>
              </Menu.Item>
              <Menu.Item icon={<LogoutOutlined />}>
                <Link to="/logout">Logout</Link>
              </Menu.Item>
            </Menu.ItemGroup>
          )}

          {user === null && (
            <>
              <Menu.Item className="menu-item" icon={<LoginOutlined />}>
                <Link to="/auth/login">Log In</Link>
              </Menu.Item>
              <Menu.Item className="menu-item" icon={<FormOutlined />}>
                <Link to="/auth/sign-up">Sign Up</Link>
              </Menu.Item>
            </>
          )}
        </Menu>
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
      <Button className="user-btn" type="text">
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

type MenuProps = { pathName: string; width: number };

type UserDropdownProps = { user: User };

export default withRouter(Navbar);
