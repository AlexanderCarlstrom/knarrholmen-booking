import { EnvironmentOutlined, ClockCircleOutlined } from '@ant-design/icons';
import { Button, Form, Input, Typography, Image, Divider } from 'antd';
import React, { useEffect, useState } from 'react';

import { fallbackImage } from '../../utils/fallbackImage';
import { activities } from '../../fakeData';
import './Activities.scss';
import { RouteComponentProps } from 'react-router-dom';

const { Title, Text } = Typography;

type Params = {
  search: string;
};

const Activities = ({ match }: RouteComponentProps<Params>) => {
  const [searchTerm, setSearchTerm] = useState('');
  const [searchResult, setSearchResult] = useState([]);
  let mounted = false;

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearchTerm(e.target.value);
  };

  useEffect(() => {
    if (!mounted) {
      const { search } = match.params;
      if (search) setSearchTerm(search);
      mounted = true;
    }
    const result = activities.filter((activity) => activity.name.toLowerCase().includes(searchTerm.toLowerCase()));
    setSearchResult(result);
  }, [searchTerm]);

  const list = searchResult.map((activity) => (
    <div key={activity.id}>
      <div className="activity">
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
    </div>
  ));

  return (
    <div className="activities">
      <div className="activities-header">
        <div className="activities-container">
          <div className="inner">
            <Title level={2} className="title">
              Find activities
            </Title>
            <Form.Item className="search-field">
              <Input
                name="search"
                placeholder="Find activities"
                size="large"
                value={searchTerm}
                onChange={handleChange}
              />
            </Form.Item>
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

export { Activities };
