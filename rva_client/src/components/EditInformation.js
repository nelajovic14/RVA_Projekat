import React from "react";
import { createAPIEndpointEdit } from "../api/index.js";
import useForm from "../useForm";
import * as ReactDOMClient from 'react-dom/client';

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

    if(values.name==''){
        values.username=props.username;
        values.password=props.password;
        values.name=props.name;
        values.lastname=props.lastname;
        console.log("if "+values.name);
    }
    console.log(values.name);
    const editovanje=e=>{
                
        e.preventDefault();
        if(validate()){
            const container = document.getElementById('root');
            const root = ReactDOMClient.createRoot(container);
            createAPIEndpointEdit('users')
            .put(values)
            .then(console.log("edit"))
            .catch(err=>console.log(err))
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
    <form onSubmit={editovanje}>                                  
        Name : <input type={"text"} name='name' value={values.name} onChange={handleInputChanges}  ></input><br/>
        Lastname : <input type={"text"} name='lastname' value={values.lastname} onChange={handleInputChanges}  ></input><br/>
        <input type={"submit"} name='promeni' value={"Promeni"} onChange={handleInputChanges}></input><br/>
    </form>
    )
}
