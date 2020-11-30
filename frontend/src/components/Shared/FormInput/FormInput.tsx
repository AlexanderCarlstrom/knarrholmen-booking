import { FieldProps } from 'formik';
import { Form, Input } from 'antd';
import 'antd/dist/antd.css';
import React from 'react';

import './FormInput.scss';

export const FormInput = (props: FieldProps & InputProps) => {
  const { field, form, label, inputType, ...rest } = props;
  const { touched, errors } = form;

  const error = touched[field.name] && errors[field.name];

  return (
    <React.Fragment>
      <label htmlFor={field.name} className="label">
        {label}:
      </label>
      <Form.Item help={error} validateStatus={error ? 'error' : undefined} className="input">
        {inputType !== null && inputType !== 'password' ? (
          <Input {...field} {...rest} />
        ) : (
          <Input.Password {...field} {...rest} />
        )}
      </Form.Item>
    </React.Fragment>
  );
};

type InputProps = {
  label: string;
  inputType: string;
};
