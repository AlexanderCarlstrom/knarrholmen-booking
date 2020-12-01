import { Form as FormikForm, Field, Formik } from 'formik';
import { Form, Button, Checkbox, Typography } from 'antd';
import { Link as RouterLink } from 'react-router-dom';
import * as Yup from 'yup';
import 'antd/dist/antd.css';
import React from 'react';

import { FormInput } from '../Shared/FormInput/FormInput';
import { publicFetch } from '../../utils/fetch';
import './Auth.scss';
import FormCheckbox from '../Shared/FormCheckbox/FormCheckbox';

const { Text, Link } = Typography;

type FormValues = {
  email: string;
  password: string;
  remember: boolean;
};

const LogInSchema = Yup.object().shape({
  email: Yup.string().email('Please enter a valid email address').required('Please enter your email address'),
  password: Yup.string()
    .min(6, 'Your password must be at least 6 characters long')
    .max(255, 'Your password cannot be longer than 255 characters')
    .required('Please enter your password'),
  remember: Yup.bool(),
});

const InitialValues = {
  email: '',
  password: '',
  remember: true,
};

const LogIn = () => {
  const submit = async (credentials: FormValues) => {
    const result = await publicFetch.post('users/login', credentials);
    console.log(result);
  };

  return (
    <React.Fragment>
      <h3 className="title">Log In</h3>
      <Formik initialValues={InitialValues} onSubmit={(values) => submit(values)} validationSchema={LogInSchema}>
        {(props) => (
          <FormikForm>
            <Field name="email" placeholder="Email address" label="Email address" component={FormInput} />
            <Field name="password" placeholder="Password" label="Password" inputType="password" component={FormInput} />
            <Field name="remember" label="Keep me logged in" component={FormCheckbox} />
            <Form.Item>
              <Button
                type="primary"
                size="large"
                shape="round"
                htmlType="submit"
                className="submit-btn"
                disabled={!props.isValid}
              >
                LOG IN
              </Button>
            </Form.Item>
          </FormikForm>
        )}
      </Formik>
      <div className="actions">
        <Link>Forgot Password?</Link>
        <Text>
          {"Don't Have an Account?"}{' '}
          <RouterLink to="/auth/sign-up" component={Link}>
            Sign Up
          </RouterLink>
        </Text>
      </div>
    </React.Fragment>
  );
};

export default LogIn;
