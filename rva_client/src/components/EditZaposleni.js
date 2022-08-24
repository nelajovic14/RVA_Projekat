import React,{ useState} from "react";
import * as ReactDOMClient from 'react-dom/client';
import Result from "./Result.js";
import useForm from "../useForm";
import axios from 'axios'

export const BASE_URL="https://localhost:44386/";

const getFreshModelObject=()=>({
    trenutnaPlata:0,
    valuta:'',
    id:0,
    brutoHonorarId:0,
    ime:'',
    godineIskustva:0,
    korisnik:''
})


export default function EditZaposleni(props){
    
    const {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChanges
    } = useForm(getFreshModelObject)

    const[plata,setPlata]=useState(0)
    const[val,setVal]=useState('')
    const[ime,setIme]=useState('')
    const[godine,setGodine]=useState(0)

    const nazad =e=>{
        e.preventDefault();
        
        const container = document.getElementById('root');
        const root = ReactDOMClient.createRoot(container);
        root.render(<Result username={props.username} password={props.password}/>)
    }

    if(plata==0 && val=='' && ime=='' && godine==0){
        
        setPlata(props.trenutnaPlata)
        setVal(props.valuta)
        setIme(props.ime)
        setGodine(props.godineIskustva)
    }
    
    const onChangePlata = (event) => {
        console.log(event.target.value)
    setPlata(event.target.value);
    };

    const onChangeVal = (event) => {
        console.log(event.target.value)
        setVal(event.target.value);
    };

    const onChangeGodine = (event) => {
        console.log(event.target.value)
        setGodine(event.target.value);
    };

    const onChangeIme=(event)=>{
        console.log(event.target.value)
        setIme(event.target.value)
    }

    const izmeni=e=>
    {
        e.preventDefault();
        values.korisnik=props.username;
        values.trenutnaPlata=plata;
        values.valuta=val;
        values.id=props.brutoId;
        values.ime=ime;
        values.godineIskustva=godine;
        
        const config = {
            headers: {  Authorization: 'Bearer ' +  localStorage.getItem('token'),}
        };
        return axios 
        .put(`${BASE_URL}api/brutohonorar`, values, config) 
        .then(response =>( values.brutoHonorarId=response.data.id,values.id=props.id,axios 
            .put(`${BASE_URL}api/zaposleni`, values, config) 
            .then(response =>(alert("Zaposleni je izmenjen"))) 
            .catch(err=>alert(err)))) 
        .catch(err=>alert(err)); 


    }
    return(
        <div class="container text-center">
                 <div class="alert alert-success"><h3><strong>Izmeni zaposlenog : </strong></h3></div><br/>
            <form onSubmit={izmeni}> 
            Ime : <input type={"text"} name='ime' value={ime}  onChange={onChangeIme}></input><br/><br/>
            Godine iskustva : <input type={"number"} name="godine" value={godine} onChange={onChangeGodine}></input><br/>
            <br/> <div class="alert alert-success"><h5>Izmeni bruto zaposlenog : </h5></div>
                Trenutna plata : <input type={"number"} name='plata'  value={plata} onChange={onChangePlata}></input><br/><br/>
                
                Valuta: <select value={val} onChange={onChangeVal}>
                <option value={"RSD"}>RSD</option>
                <option value={"EUR"}>EUR</option>
                <option value={"KM"}>KM</option>
                </select> <br/><br/><br/>
                
                <input type={"submit"} name='izmeni' value={"Izmeni"} class="btn btn-success"></input> &nbsp;&nbsp;
            <input type={"button"} name='back' value={"Nazad"} onClick={nazad} class="btn btn-success"></input>
            </form>
        </div>
    )


}