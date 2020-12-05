import { FieldProps } from 'formik';
import { Form, Input } from 'antd';
import 'antd/dist/antd.css';
import React from 'react';

import './FormInput.scss';
import { SizeType } from 'antd/lib/config-provider/SizeContext';

export const FormInput = (props: FieldProps & InputProps) => {
  const { field, form, label, inputType, size, action, ...rest } = props;
  const { touched, errors } = form;

  const error = touched[field.name] && errors[field.name];

  return (
    <React.Fragment>
      <div className="top-bar">
        <label htmlFor={field.name} className="label">
          {label}:
        </label>
        {action !== null && action}
      </div>
      <Form.Item help={error} validateStatus={error ? 'error' : undefined} className="input">
        {inputType !== null && inputType !== 'password' ? (
          <Input size={size} {...field} {...rest} />
        ) : (
          <Input.Password size={size} {...field} {...rest} />
        )}
      </Form.Item>
    </React.Fragment>
  );
};

type InputProps = {
  label: string;
  inputType: string;
  size: SizeType;
  action?: React.ReactNode;
};
