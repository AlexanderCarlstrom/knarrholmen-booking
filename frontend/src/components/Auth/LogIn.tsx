import { withFormik, Form as FormikForm, Field } from 'formik';
import { Form, Button, Checkbox, Typography } from 'antd';
import { Link as RouterLink } from 'react-router-dom';
import * as Yup from 'yup';
import 'antd/dist/antd.css';
import React from 'react';

import './Auth.scss';
import { FormInput } from '../Shared/FormInput/FormInput';

const { Text, Link } = Typography;

const LogInForm = () => {
  return (
    <React.Fragment>
      <h3 className="title">Log In</h3>
      <FormikForm>
        <Field name="email" placeholder="Email address" label="Email address" component={FormInput} />
        <Field name="password" placeholder="Password" label="Password" inputType="password" component={FormInput} />
        <Form.Item valuePropName="checked">
          <Checkbox name="remember">Remember me</Checkbox>
        </Form.Item>
        <Form.Item>
          <Button type="primary" htmlType="submit" className="submit-btn" disabled={false}>
            SIGN IN
          </Button>
        </Form.Item>
      </FormikForm>
      <div className="actions">
        <Link>Forgot Password?</Link>
        <Text>
          {"Don't Have an Account?"}{' '}
          <Link>
            <RouterLink to="/auth/sign-up">Sign Up</RouterLink>
          </Link>
        </Text>
      </div>
    </React.Fragment>
  );
};

type FormValues = {
  email: string;
  password: string;
  remember: boolean;
};

type LogInProps = {
  initialEmail?: string;
};

const LogInSchema = Yup.object().shape({
  email: Yup.string().email('Please enter a valid email address').required('Please enter your email address'),
  password: Yup.string()
    .min(6, 'Your password must be at least 6 characters long')
    .max(255, 'Your password cannot be longer than 255 characters')
    .required('Please enter your password'),
  remember: Yup.bool(),
});

const LogIn = withFormik<LogInProps, FormValues>({
  mapPropsToValues: (props) => {
    return {
      email: props.initialEmail || '',
      password: '',
      remember: true,
    };
  },
  validationSchema: LogInSchema,
  handleSubmit: (values) => {
    console.log(values);
  },
})(LogInForm);

export default LogIn;
