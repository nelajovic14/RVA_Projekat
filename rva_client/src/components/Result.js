import React,{useState} from 'react'
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
import Logs from './Logs';


const getFreshModelObject=()=>({
    username:'',
    password:'',
    name:'',
    lastName:'',
    uloga:''
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

      return(

            <BrowserRouter>
            <Routes>
           <Route path="/" element={<Layout />}>      
            <Route path='Register' element={<Register username={values.username} />} />
            <Route path='EditInformation'   element={<Edit username={values.username} password={values.password}/>} />
            <Route path='Login' element={<Logout username={values.username}/>}  />
            <Route path='NetoHonorari' element ={<Neto  username={values.username} />} />
            <Route path='BrutoHonorari' element ={<Bruto username={values.username}   />}/>
            <Route path='Zaposleni' element ={<Zapolseni  username={values.username}  />} />
            <Route path='Logs' element ={<Logs  username={values.username}/> } />
          </Route>
              </Routes>
              </BrowserRouter>
        )

    }
    
