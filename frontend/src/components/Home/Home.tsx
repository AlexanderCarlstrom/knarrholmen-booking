import { Typography, Input } from 'antd';
import React from 'react';
import { RouteComponentProps } from 'react-router-dom';

import './Home.scss';

const Home = ({ history }: RouteComponentProps) => {
  const search = (search: string) => {
    history.push(`/activities/${search}`);
  };

  return (
    <div className="home">
      <div className="first">
        <div className="content">
          <div className="search-container">
            <Input.Search
              placeholder="Find activities"
              onSearch={search}
              enterButton="Search"
              size="large"
              className="search-field"
            />
          </div>
        </div>
      </div>
      <div className="second">
        <Typography.Title>Knarrholmen Booking</Typography.Title>
        <Typography.Title level={4}>A booking system for various activities on Knarrholmen</Typography.Title>
      </div>
    </div>
  );
};

export default Home;
