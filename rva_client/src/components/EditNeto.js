import React,{useState,useRef} from "react";
import {  dodajBruto, izmeniNeto} from "../api/index.js";
import * as ReactDOMClient from 'react-dom/client';
import Result from "./Result.js";
import UseForm from "../useForm";
import axios from 'axios'
import { Radio } from "@mui/material";

export const BASE_URL="https://localhost:44386/";

const getFreshModelObject=()=>({
    trenutnaPlata:0,
    valuta:'',
    id:0,
    uvecanje:'',
    umanjenje:'',
    porezi:[],
    brutoHonorarId:0,
    korisnik:'',
    vremeSlanjaNaFront:'',
    vremeZaIzmenu:''
})

var opcije = [];
export default function IzmeniNetoHonorar(props){
    

    const[plata,setPlata]=useState(0)
    const[val,setVal]=useState('')
    const[porezi,setPoreze]=useState([])
    const[uvecanje,setUvecanje]=useState('')
    const[umanjenje,setUmanjenje]=useState('')
    const[konlikt,setKonflikt]=useState(<div></div>)
    const[rezultat,setRezultat]=useState('podaci')
    

        
    const {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChanges
    } = UseForm(getFreshModelObject)

    if(plata==0 && val=='' && uvecanje=='' && umanjenje==''){
        var today = new Date();
        var datum = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate().toLocaleString();
        var time = today.getHours() + ':' + today.getMinutes() + ':' + today.getSeconds().toLocaleString();
        values.vremeSlanjaNaFront=datum+" "+time;

        setPlata(props.trenutnaPlata)
        setVal(props.valuta)
        setUmanjenje(props.umanjenje)
        setUvecanje(props.uvecanje)
        setPoreze(props.porezi)
        opcije=[];
        console.log(props.uvecanje);
        console.log(props.umanjenje);
    }
    console.log(plata,val,porezi,uvecanje,umanjenje)
    const nazad =e=>{
        e.preventDefault();
        
        const container = document.getElementById('root');
        const root = ReactDOMClient.createRoot(container);
        root.render(<Result username={props.username} password={props.password}/>)
    }


    
    const onChangePlata = (event) => {
        console.log(event.target.value)
    setPlata(event.target.value);
    };

    const onChangeVal = (event) => {
        console.log(event.target.value)
    setVal(event.target.value);
    };

    
    const changePorez = function(event,selectedItems){
        if(opcije.includes(selectedItems)){
            for(var i=0;i<opcije.length;i++){
                if(opcije[i]==selectedItems){
                    opcije.splice(i,1);
                    return;
                }
            }
        }
        console.log(event.target.value,selectedItems,opcije)
        opcije.push(selectedItems)
        setPoreze(opcije);
    }
    

    const changeUmanjenje=(event)=>{
        console.log(event.target.value)
        setUmanjenje(event.target.value)
    }

    const changeUvecanje=(event)=>{
        console.log(event.target.value)
        setUvecanje(event.target.value);
    }

    const handleDate=e=>{
        e.preventDefault();
        var today = new Date();
        var datum = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate().toLocaleString();
        var time = today.getHours() + ':' + today.getMinutes() + ':' + today.getSeconds().toLocaleString();
        values.vremeSlanjaNaFront=datum+" "+time;
        const config = {
            headers: {  Authorization: 'Bearer ' +  localStorage.getItem('token'),}
        };
        return axios 
                .put(`${BASE_URL}api/brutohonorar`, values, config) 
                .then(response =>( values.brutoHonorarId=response.data.id,values.id=props.id,axios 
                    .put(`${BASE_URL}api/netohonorar`, values, config) 
                    .then(response =>(handleResponse(response))) 
                    .catch(err=>("Konflikt")))) 
                .catch(err=>alert(err)); 
    }

    const izmeni=e=>
    {
        
        e.preventDefault();
        if(!plata){
            alert("Plata mora biti uneta!")
            return;
        }
        if(!porezi){
            alert("Porezi moraju biti uneti!")
            return;
        }

        values.korisnik=props.username;
        values.trenutnaPlata=plata;
        values.valuta=val;
        values.id=props.idBruta;
        values.uvecanje=uvecanje;
        values.umanjenje=umanjenje;
        values.porezi=porezi;
        var today = new Date();
        var datum = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate().toLocaleString();
        var time = today.getHours() + ':' + today.getMinutes() + ':' + today.getSeconds().toLocaleString();
        values.vremeZaIzmenu=datum+" "+time;

        const config = {
            headers: {  Authorization: 'Bearer ' +  localStorage.getItem('token'),}
        };
        return axios 
                .put(`${BASE_URL}api/brutohonorar`, values, config) 
                .then(response =>( values.brutoHonorarId=response.data.id,values.id=props.id,axios 
                    .put(`${BASE_URL}api/netohonorar`, values, config) 
                    .then(response =>(handleResponse(response))) 
                    .catch(err=>("Konflikt")))) 
                .catch(err=>alert(err)); 

    }

    const handleResponse=(response)=>{
        if(response.data.id==-1){
            alert("Konflikt")
            setKonflikt(<div>Izaberite opciju : <button onClick={nazad}>Pregazi moje izmene</button> <button onClick={handleDate}>Pregazi tuđe izmene</button></div>)
        }
        else{
            alert("Neto honorar je izmenjen!")
        }
    }

    const p=useRef();

    return(
             <div class="container text-center">
           <div class="alert alert-success"> <h3>Izmeni neto honorar : </h3> </div>
               
            <form onSubmit={izmeni}> 
            Porezi : <select multiple={true} value={porezi} ref={p} onChange={(e)=> {changePorez(e,p.current.value)}}>
            <option value={"POTROSNJA"}>POTROSNJA</option>
            <option value={"DOHODAK"}>DOHODAK</option>
            <option value={"DOBIT"}>DOBIT</option>
            <option value={"IMOVINA"}>IMOVINA</option>
        </select><br/><br/>
        Uvećanje : <select  value={uvecanje} onChange={changeUvecanje}>
            <option value={"PET"}>5 %</option>
            <option value={"DESET"}>10 %</option>
            <option value={"DVADESET"}>20 %</option>
            <option value={"PEDESET"}>50 %</option>
            <option value={"SEDAMDESET"}>70 %</option>
            <option value={"STO"}>100 %</option>
        </select><br/><br/>
        Umanjenje : <select  value={umanjenje} onChange={changeUmanjenje} >
            <option value={"PET"}>5 %</option>
            <option value={"DESET"}>10 %</option>
            <option value={"DVADESET"}>20 %</option>
            <option value={"PEDESET"}>50 %</option>
            <option value={"SEDAMDESET"}>70 %</option>
            <option value={"STO"}>100 %</option>
        </select><br/><br/>

        <br/><div class="alert alert-success"><h5>Izmeni bruto honorar neto honorara : </h5></div>
        Trenutna plata : <input type={"number"} name='plata'  value={plata} onChange={onChangePlata}></input><br/><br/>
                
                Valuta: <select value={val} onChange={onChangeVal}>
                <option value={"RSD"}>RSD</option>
                <option value={"EUR"}>EUR</option>
                <option value={"KM"}>KM</option>
                </select> <br/><br/>

                
                <input type={"submit"} name='izmeni' value={"Izmeni"} class="btn btn-success"></input> &nbsp;&nbsp;
            <input type={"button"} name='back' value={"Nazad"} onClick={nazad} class="btn btn-success"></input><br/>
            </form>
            <br/><br/>
            {konlikt}
        </div>
    )


}