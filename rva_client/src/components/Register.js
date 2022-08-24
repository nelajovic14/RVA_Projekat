import React,{useRef,useState} from "react";
import * as ReactDOMClient from 'react-dom/client';
import useForm from "../useForm";
import axios from 'axios'

export const BASE_URL="https://localhost:44386/";

const getFreshModelObject=()=>({
    username:'',
    password:'',
    name:'',
    lastname:'',
    uloga:'',
    korisnik:''
})

export default function Register(props){
    const {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChanges
    } = useForm(getFreshModelObject)

    const [alertMessage,setAlert]=useState(<div></div>);

    const uloga=useRef();

        const register=e=>{
            
            e.preventDefault();
            if(validate()){
                values.uloga=uloga.current.value;
                const container = document.getElementById('root');
                const root = ReactDOMClient.createRoot(container);
                values.korisnik=props.username;
               
                   const config = {
                        headers: {  Authorization: 'Bearer ' +  localStorage.getItem('token'),}
                    };
                  return axios 
                    .post(`${BASE_URL}api/users/register`, values, config) 
                    .then(res=>(setAlert(<div class="alert alert-success">
                    <strong>Success!</strong> 
                  </div>))) 
                    .catch(err=>(console.log(err),setAlert(<div class="alert alert-danger">
                    <strong>Can't register because you are not admin!</strong> 
                  </div>))); 
             
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
        <div class="jumbotron text-center">
            <h3>Register new user:</h3><br/><br/>
        <form onSubmit={register}> 
            Username : <input type={"text"} name='username' value={values.username} onChange={handleInputChanges}  ></input><br/><br/>
            
            Password: <input type={"password"} name='password' value={values.password} onChange={handleInputChanges}></input><br/><br/>
            
            Name : <input type={"text"} name='name' value={values.name} onChange={handleInputChanges}  ></input><br/><br/>

            Lastname : <input type={"text"} name='lastname' value={values.lastname} onChange={handleInputChanges}  ></input><br/><br/>

            Role : <select ref={uloga} >
                <option value={'ADMIN'}>ADMIN</option>
                <option value={'KORISNIK'}>KORISNIK</option>
            </select><br/><br/>

            <input type={"submit"} name='registruj' value={"Register"} onChange={handleInputChanges}></input><br/>
        </form>
        <br/>
        {alertMessage}
        </div>
    )
       
}