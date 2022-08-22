import React,{useState} from "react";
import {getLogs} from '../api/index'
import useForm from "../useForm";

const getFreshModelObject=()=>({
    username:'',
    password:'',
})

export default function Logs(props){
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
    const [lista,setListu]=useState([]);

    getLogs('logger')
    .post(values)
    .then(res=>(setListu(res.data)))

    const elementi=lista.map((element)=>
        <tr><td>{element.dogadjaj}</td><td>{element.poruka}</td></tr>
    )

    return(
        <div class="container text-center">
            <table class="table">
                <tr><td><b>DOGAĐAJ</b></td><td><b>PORUKA</b></td></tr>
                {elementi}
            </table>
        </div>
    )

}