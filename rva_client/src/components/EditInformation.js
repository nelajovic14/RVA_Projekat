import React,{useState} from "react";
import { createAPIEndpointEdit } from "../api/index.js";
import useForm from "../useForm";
import * as ReactDOMClient from 'react-dom/client';

const getFreshModelObject=()=>({
    username:'',
    password:'',
    name:'',
    lastname:'',
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
    }
    const [alertMessage,setAlert]=useState(<div></div>);

    const editovanje=e=>{
                
        e.preventDefault();
        if(validate()){
            const container = document.getElementById('root');
            const root = ReactDOMClient.createRoot(container);
            createAPIEndpointEdit('users')
            .put(values)
            .then(console.log("edit"),setAlert(<div class="alert alert-success">
            <strong>Success!</strong> </div>))
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
        Name : <input type={"text"} name='name' value={values.name} onChange={handleInputChanges}  ></input><br/><br/>
        Lastname : <input type={"text"} name='lastname' value={values.lastname} onChange={handleInputChanges}  ></input><br/><br/>
        <input type={"submit"} name='promeni' value={"Change"} onChange={handleInputChanges}></input><br/>
    </form><br/>{alertMessage}
    </div>
    )
}
