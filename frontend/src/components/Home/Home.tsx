import React, { useEffect, useState } from 'react';
import { RouteComponentProps } from 'react-router-dom';

import './Home.scss';
import { Button, Input } from 'antd';
import { publicFetch } from '../../utils/axios';
import { ActivitiesResponse } from '../../types/ApiReponse';
import { Activities } from '../../types/Activities';

const Home = ({ history }: RouteComponentProps) => {
  const [activities, setActivities] = useState([]);
  const search = (search: string) => {
    history.push(`/activities/${search}`);
  };

  useEffect(() => {
    publicFetch
      .get<ActivitiesResponse>('activities', { params: { search: '', start: 0, limit: 3 } })
      .then((a) => setActivities(a.data.activities));
  }, []);

  const listActivities = activities.map((a: Activities) => {
    const open = a.open < 10 ? '0' + a.open + ':00' : a.open + ':00';
    const close = a.close < 10 ? '0' + a.close + ':00' : a.close + ':00';
    return (
      <div className="activity" key={a.id} onClick={() => navigateToActivity(a.id)}>
        <div className="img" />

        <div className="info">
          <span className="title">{a.name}</span>
          <span className="location">{a.location}</span>
          <span className="open">{open + ' - ' + close}</span>
        </div>
      </div>
    );
  });

  const navigateToActivity = (id: string) => {
    history.push('/activity/' + id);
  };

  return (
    <div className="home">
      <div className="container">
        <Input placeholder="Find activities..." className="search-field" />
        <div className="activity-list">{listActivities}</div>
        <Button className="showMore">Show More</Button>
      </div>
    </div>
  );
};

export default Home;
