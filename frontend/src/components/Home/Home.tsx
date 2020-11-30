import { Typography, Input } from 'antd';
import React from 'react';

import './Home.scss';

const Home = () => {
  const search = (search: string) => {
    console.log(search);
  };

  return (
    <div className="home">
      <div className="first">
        <div className="content">
          <div className="search-container">
            <Input.Search
              placeholder="Search activities"
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
