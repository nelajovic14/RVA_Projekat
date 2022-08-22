import React,{useState} from "react";

export default function UseForm(getFreshModelObject){

    const[values,setValues]=useState(getFreshModelObject());
    const[errors,setErrors]=useState({})

    const handleInputChanges = e=>{
        const{name,value}=e.target
        setValues({
            ...values,
            [name]:value
        })
    }
    return {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChanges
    }

}