import React,{useState} from "react";
import { createAPIEndpointEdit } from "../api/index.js";
import useForm from "../useForm";
import * as ReactDOMClient from 'react-dom/client';
import axios from 'axios'

export const BASE_URL="https://localhost:44386/";

const getFreshModelObject=()=>({
    username:'',
    password:'',
    name:'',
    lastname:''
})

export default function Edit(props){
        
    var {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChanges
    } = useForm(getFreshModelObject)


    const [isChanged,setIsChanged]=useState(true);
    const [alertMessage,setAlert]=useState(<div></div>);
    
    if(isChanged){
        const config = {
            headers: {  Authorization: 'Bearer ' +  localStorage.getItem('token'),}
        };
        axios 
        .post(`${BASE_URL}api/users/getUser`, {username:props.username}, config) 
        .then(res=>(setValues(res.data),setIsChanged(false)))
        .catch(err=>alert(err))
    }

    const editovanje=e=>{
                
        e.preventDefault();
        if(validate()){
            const config = {
                headers: {  Authorization: 'Bearer ' +  localStorage.getItem('token'),}
            };
             axios 
            .put(`${BASE_URL}api/users`, values, config) 
            .then(res=>(setAlert(<div class="alert alert-success"><strong>Success!</strong> </div>),setIsChanged(true))) 
            .catch(err=>(console.log(err),setAlert(<div class="alert alert-danger">
            <strong>Can't chane your info! {err}</strong> </div>)))

        }
    }
    let nameError="";
    let lastnameError="";

    const validate=()=>{

        if (!values.name) {          
            nameError = "Name field is required";
            alert(nameError)
        }

        if (!values.lastname) {
            lastnameError = "Lastname field is required";
            alert(lastnameError)
        }

        if (nameError || lastnameError) {
            return false;
        }
        return true;
    }

    return(
        <div class="jumbotron text-center">
            <h3>Change information about you : </h3><br/>
    <form onSubmit={editovanje}>                                  
        Name : <input type={"text"} name='name' value={values.name}  onChange={handleInputChanges}  ></input><br/><br/>
        Lastname : <input type={"text"} name='lastname' value={values.lastname}  onChange={handleInputChanges}></input><br/><br/>
        <input type={"submit"} name='promeni' value={"Change"} ></input><br/>
    </form><br/>{alertMessage}
    </div>
    )
}
