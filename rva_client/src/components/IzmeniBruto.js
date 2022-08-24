import React,{useRef, useState} from "react";
import * as ReactDOMClient from 'react-dom/client';
import Result from "./Result.js";
import useForm from "../useForm";
import axios from 'axios'

export const BASE_URL="https://localhost:44386/";

const getFreshModelObject=()=>({
    trenutnaPlata:0,
    valuta:'',
    id:0,
    korisnik:''
})


export default function IzmeniBrutoHonorar(props){
    
    const {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChanges
    } = useForm(getFreshModelObject)

    const[plata,setPlata]=useState(0)
    const[val,setVal]=useState('')

    const nazad =e=>{
        e.preventDefault();
        
        const container = document.getElementById('root');
        const root = ReactDOMClient.createRoot(container);
        root.render(<Result username={props.username} password={props.password}/>)
    }

    console.log("props:"+props.trenutnaPlata+"/"+props.valuta)
        if(plata==0 && val==''){
            console.log(props.trenutnaPlata,props.valuta)
            setPlata(props.trenutnaPlata)
            setVal(props.valuta)
            console.log("pocetna : "+plata+"/"+val)
        }
        
        const onChangePlata = (event) => {
            console.log(event.target.value)
        setPlata(event.target.value);
        };

        const onChangeVal = (event) => {
            console.log(event.target.value)
        setVal(event.target.value);
        };

        const izmeni=e=>
        {
            e.preventDefault();
            values.korisnik=props.username;
            values.trenutnaPlata=plata;
            values.valuta=val;
            values.id=props.id;

            const config = {
                headers: {  Authorization: 'Bearer ' +  localStorage.getItem('token'),}
            };
          return axios 
            .put(`${BASE_URL}api/brutohonorar`, values, config) 
            .then(res=>alert("Bruto honorar je izmenjen")) 
            .catch(err=>alert(err)); 

        }

        return(
                    <div class="container text-center">
                    <div class="alert alert-success"> <h3>Izmeni bruto honorar :</h3> </div><br/><br/>
                <form onSubmit={izmeni}> 
                    Trenutna plata : <input type={"number"} name='plata'  value={plata} onChange={onChangePlata}></input><br/><br/>
                    
                    Valuta: <select value={val} onChange={onChangeVal}>
                    <option value={"RSD"}>RSD</option>
                    <option value={"EUR"}>EUR</option>
                    <option value={"KM"}>KM</option>
                    </select> <br/><br/><br/>
                    
                    <input type={"submit"} name='izmeni' value={"Izmeni"} class="btn btn-success"></input> &nbsp;&nbsp;
                    
                    <input type={"button"} name='back' value={"Nazad"} onClick={nazad} class="btn btn-success"></input><br/>
                </form>
            </div>
            )


}