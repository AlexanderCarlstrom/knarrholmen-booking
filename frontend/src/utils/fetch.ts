import axios from 'axios';

export const publicFetch = axios.create({ baseURL: process.env.API_URL });
