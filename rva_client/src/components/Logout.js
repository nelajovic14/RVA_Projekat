import React from "react";
import { LogOutUser } from "../api";
import useForm from "../useForm";

const getFreshModelObject=()=>({
    username:'',
    password:''
})

export default function Logout(props){
    
    const {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChanges
    } = useForm(getFreshModelObject)

    if(values.username==''){
        values.username=props.username;
    }
    LogOutUser('users')
    .post(values)
    .then(window.location.href="http://localhost:3000")

    

}