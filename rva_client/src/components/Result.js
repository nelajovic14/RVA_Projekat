import React from 'react'
import Register from './Register'
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Login from './Login';
import Layout from './Layout';
import Edit from './EditInformation';
import * as ReactDOMClient from 'react-dom/client';
import { createAPIEndpointgetUser } from "../api/index.js";
import useForm from "../useForm";
import { render } from '@testing-library/react';

const getFreshModelObject=()=>({
    username:'',
    password:'',
    name:'',
    lastname:''
})

export default function Result(props){
    const {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChanges
    } = useForm(getFreshModelObject)

    const container = document.getElementById('root');
    const root = ReactDOMClient.createRoot(container);

    values.username=props.username;
    values.password=props.password;
    createAPIEndpointgetUser('users')
        .post(values)
        .then(res=>(values.name=res.data.name,values.lastname=res.data.lastName,root.render(<BrowserRouter>
            <Routes>
            <Route path="/" element={<Layout />}>
              <Route path='Register' element={<Register />} />
              <Route path='Login' element={<Login />} />
              <Route path='EditInformation' element={<Edit name={values.name} lastname={values.lastname} username={values.username} password={values.password}/>} />
          </Route>
              </Routes>
              </BrowserRouter>),console.log(values.name)));

}