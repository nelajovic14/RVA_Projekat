import React from "react";
import ReactDOM from 'react-dom'
import { createAPIEndpointRegister } from "../api/index.js";
import { createRoot } from 'react-dom/client';
import useForm from "../useForm";
import Result from "./Result.js";
import jwt_decode from "jwt-decode";

const getFreshModelObject=()=>({
    username:'',
    password:'',
    name:'',
    lastname:''
})

export default function Register(){
    const {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChanges
    } = useForm(getFreshModelObject)

        const register=e=>{
            
            e.preventDefault();
            if(validate()){
                const container = document.getElementById('root');
                const root = createRoot(container);
                var decoded;
                createAPIEndpointRegister('users')
                .post(values)
                .then(res=>(console.log(res),root.render(<Result/>)))
                .catch(err=>console.log(err))


            }
        }
        let usernameError = "";
        let passwordError = "";
        let nameError="";
        let lastnameError="";

        const validate=()=>{
            
            if (!values.username) {
                usernameError = "Username field is required";
                alert(usernameError)
            }
            
            if (!values.password) {
                passwordError = "Password field is required";
                alert(passwordError)
            }

            if (!values.name) {
                nameError = "Name field is required";
                alert(nameError)
            }

            if (!values.lastname) {
                lastnameError = "Lastname field is required";
                alert(lastnameError)
            }

            if (nameError || passwordError || usernameError || lastnameError) {
                return false;
            }
            return true;
        }


    return(
        <form onSubmit={register}> 
            Username : <input type={"text"} name='username' value={values.username} onChange={handleInputChanges}  ></input><br/>
            
            Password: <input type={"password"} name='password' value={values.password} onChange={handleInputChanges}></input><br/>
            
            Name : <input type={"text"} name='name' value={values.name} onChange={handleInputChanges}  ></input><br/>

            Lastname : <input type={"text"} name='lastname' value={values.lastname} onChange={handleInputChanges}  ></input><br/>
            <input type={"submit"} name='registruj' value={"Register"} onChange={handleInputChanges}></input><br/>
        </form>
    )
}