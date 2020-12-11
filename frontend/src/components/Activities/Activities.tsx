import { EnvironmentOutlined, ClockCircleOutlined } from '@ant-design/icons';
import { Button, Form, Input, Typography, Image, Divider } from 'antd';
import React from 'react';

import { fallbackImage } from '../../utils/fallbackImage';
import { activities } from '../../fakeData';
import './Activities.scss';

const { Title, Text } = Typography;

const Activities = () => {
  const list = activities.map((activity) => (
    <>
      <div className="activity" key={activity.id}>
        <Image src={activity.img} fallback={fallbackImage} className="activity-img" />
        <div className="activity-content">
          <Title level={4} className="title">
            {activity.name}
          </Title>
          <ul className="info">
            <li>
              <Text type="secondary">
                <EnvironmentOutlined className="icon-margin" />
                {activity.location}
              </Text>
            </li>
            <li>
              <Text type="secondary" className="opening-hours">
                <ClockCircleOutlined className="icon-margin" />
                {activity.open + '-' + activity.close}
              </Text>
            </li>
          </ul>
          <Title level={5} className="price no-margin">
            {activity.price}kr
          </Title>
          <Button type="primary" className="activity-btn no-margin">
            SEE MORE
          </Button>
        </div>
      </div>
      <Divider />
    </>
  ));

  return (
    <div className="activities">
      <div className="activities-header">
        <div className="activities-container">
          <div className="inner">
            <Title level={2} className="title">
              Find activities
            </Title>
            <Form onFinish={onSearch} className="inline-form">
              <Form.Item className="search-field">
                <Input name="search" placeholder="Find activities" size="large" />
              </Form.Item>
              <Form.Item className="search-btn">
                <Button type="primary" size="large">
                  SEARCH
                </Button>
              </Form.Item>
            </Form>
          </div>
        </div>
      </div>
      <div className="activities-content">
        <div className="activities-container">
          <div className="list">{list}</div>
        </div>
      </div>
    </div>
  );
};

const onSearch = (search: string) => {
  console.log(search);
};

export { Activities, onSearch };
