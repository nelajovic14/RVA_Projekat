import React from "react";
import ReactDOM from 'react-dom'
import { createAPIEndpoint } from "../api/index.js";
import useForm from "../useForm";
import Result from "./Result.js";
import jwt_decode from "jwt-decode";

import { createRoot } from 'react-dom/client';
import Register from "./Register.js";


const getFreshModelObject=()=>({
    username:'',
    password:''
})
var decode;
export default function Login(){
    const {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChanges
    } = useForm(getFreshModelObject)

        const login=e=>{
            
            e.preventDefault();
            if(validate()){
                const container = document.getElementById('root');
                const root = createRoot(container);
                var decoded;
                createAPIEndpoint('users')
                .post(values)
                .then(res=>(console.log(res),decoded=jwt_decode(res.data),console.log(decoded),root.render(<Register/>)))
                .catch(err=>console.log(err))


            }
        }
        let nameError = "";
        let passwordError = "";
        const validate=()=>{
            
            if (!values.username) {
                nameError = "Name field is required";
                alert(nameError)
            }
            
            if (!values.password) {
                passwordError = "Password field is required";
                alert(passwordError)
            }
            if (nameError || passwordError) {
                return false;
            }
            return true;
        }


    return(
        <form onSubmit={login}> 
            <input type={"text"} name='username' value={values.username} onChange={handleInputChanges}  ></input><br/>
            
            <input type={"password"} name='password' value={values.password} onChange={handleInputChanges}></input><br/>
            
            <input type={"submit"} name='uloguj' value={"Log in"} onChange={handleInputChanges}></input><br/>
        </form>
    )
}