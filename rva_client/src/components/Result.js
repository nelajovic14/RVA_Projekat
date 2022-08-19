import React from 'react'
import Register from './Register'
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Layout from './Layout';
import Edit from './EditInformation';
import * as ReactDOMClient from 'react-dom/client';
import { createAPIEndpointgetUser } from "../api/index.js";
import useForm from "../useForm";
import Logout from './Logout';
import Neto from './NetoHonorari';
import Bruto from './BrutoHonorari';
import Zapolseni from "./Zaposleni.js"

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
        .then(res=>(values.name=res.data.name,values.lastname=res.data.lastName,root.render(

                    <BrowserRouter>
                    <Routes>
                    <Route path="/" element={<Layout />}>
                      <Route path='Register' element={<Register />} />
                      <Route path='EditInformation' element={<Edit name={values.name} lastname={values.lastname} username={values.username} password={values.password}/>} />
                      <Route path='Login' element={<Logout />} />
                      <Route path='NetoHonorari' element ={<Neto />}/>
                      <Route path='BrutoHonorari' element ={<Bruto username={values.username} password={values.password}/>}/>
                      <Route path='Zaposleni' element ={<Zapolseni />}/>
                  </Route>
                      </Routes>
                      </BrowserRouter>
              )))
}