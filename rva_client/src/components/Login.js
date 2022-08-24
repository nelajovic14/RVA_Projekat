import React,{useState} from "react";
import { createAPIEndpoint } from "../api/index.js";
import useForm from "../useForm";
import Result from "./Result.js";
import jwt_decode from "jwt-decode";
import * as ReactDOMClient from 'react-dom/client';


const getFreshModelObject=()=>({
    username:'',
    password:'',
    name:'',
    lastname:'',
    uloga:'',
})

export default function Login(){
    const {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChanges
    } = useForm(getFreshModelObject)
    const [alertMessage,setAlert]=useState(<div></div>);
        const login=e=>{
            
            e.preventDefault();
            if(validate()){
                
                const container = document.getElementById('root');
                const root = ReactDOMClient.createRoot(container);
                var decoded='';
                createAPIEndpoint('users')
                .post(values)
                .then(res=>(console.log(res.data),decoded=jwt_decode(res.data),localStorage.setItem('token',res.data),root.render(<Result username={values.username} password={values.password} />)))
                .catch(err=>(console.log("error:"+err),setAlert(<div class="alert alert-danger">
                <strong>Wrong username or password!</strong> 
              </div>)))
               
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
        <div class="jumbotron text-center">  
            <h2 class="bg-info">Please, log in :)</h2><br/><br/>
            <p >
        <form onSubmit={login} > 
           <label> Username : </label>&nbsp;<input type={"text"} name='username'  value={values.username} onChange={handleInputChanges}  ></input><br/><br/>
            
           <label> Password : </label>&nbsp;<input type={"password"} name='password' value={values.password} onChange={handleInputChanges}></input><br/><br/>
            
            <input type={"submit"} name='uloguj' value={"Log in"} onChange={handleInputChanges} class="btn btn-info"></input><br/>
        </form></p>
        {alertMessage}
        </div>
    )
}