import React from "react";
import {useState,useRef} from "react";
import { getBrutoFromNeto,getNetos,obrisiNeto,pretragaNeto,DuplirajNeto } from "../api/index.js";
import DodajNetoHonorar from "./DodajNetoHonorar"
import * as ReactDOMClient from 'react-dom/client';
import useForm from "../useForm";
import IzmeniNetoHonorar from './EditNeto'
import axios from 'axios'

const BASE_URL="https://localhost:44386/";

const getFreshModelObject=()=>({
    porezi:null,
    uvecanje:null,
    umanjenje:null,
    trenutnaPlata:0,
    valuta:'',
    korisnik:''
})

export default function Neto(props){

    const {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChanges
    } = useForm(getFreshModelObject)

    const [elements, setElement] = useState([]);
    const [brutoElement, setBruto]=useState(null)
    const [isChanged,setIsChanged]=useState(true)
    const [pretrazeni,setPretrazene]=useState([])
   const [prikazPretrazenih,setPrikaz]=useState(<div></div>)

   
   const container = document.getElementById('root');
   const root = ReactDOMClient.createRoot(container);

    const porezi=useRef();
    const uvecanje=useRef();
    const umanjenje=useRef();



    const pretraga =e=>{
        e.preventDefault();
        values.korisnik=props.username;
        console.log(porezi.current.value,uvecanje.current.value,umanjenje.current.value)
        if((porezi.current.value.trim(' ')=='') && (uvecanje.current.value.trim(' ')=='') && (umanjenje.current.value.trim(' ')=='')){
            alert("Unesite neko polje za pretragu");
            setIsChanged(true)
            return;
        }
        if(!(uvecanje.current.value.trim(' ')=='')){
        values.uvecanje=uvecanje.current.value;
        console.log("uvecanje")
        }
        else{
            values.uvecanje=null;
        }
        if(!(umanjenje.current.value.trim(' ')=='')){
        values.umanjenje=umanjenje.current.value;
        console.log("umanjenje")
        }
        else{
            values.umanjenje=null;
        }

        pretragaNeto('netohonorar')
        .post(values)
        .then(response=>(console.log(response.data),setElement(response.data)))

        
    }

    var opcije = [];
    const handleSelect = function(e,selectedItems) {
        opcije.push(selectedItems)
        values.porezi=opcije;
    }

    const dodaj=e=>{
        e.preventDefault();
        root.render(<DodajNetoHonorar username={props.username} password={props.password} />)
    }

    const findBruto=(event,element)=>{
       
        event.preventDefault();
        getBrutoFromNeto("brutohonorar")
        .post(element)
        .then(res=>(console.log(res.data),setBruto(res.data)))
        
    }

    const obrisi=(event,element)=>{
        element.korisnik=props.username;
        event.preventDefault();
        obrisiNeto("netohonorar")
        .post(element)
        .then(setIsChanged(true),axios.get(BASE_URL+'api/netohonorar')
        .then(response => (setElement(response.data),setIsChanged(true))))
    }

    var brutoPrikaz=<div></div>;

    if(brutoElement!=null){
        brutoPrikaz=<div class="jumbotron">Id : {brutoElement.id} <br/> Trenutna plata : {brutoElement.trenutnaPlata} &nbsp; {brutoElement.valuta}</div>
    }

    const ispisiPoreze=(porezi)=>{
        const ispis=porezi.map(
            (el)=><div>{el}</div>
        )
        return ispis;
    }

    if(isChanged){
        axios.get(BASE_URL+'api/netohonorar')
        .then(response => (setElement(response.data),setIsChanged(false)))
        

    }

    const dupliraj=(event,element)=>{
        event.preventDefault();
        console.log(element);
        DuplirajNeto('netohonorar')
        .post(element)
        .then(
            getNetos('netohonorar')
        .post()
        .then(response => (console.log(response.data),setElement(response.data),setIsChanged(true)))

        );
    }

    const izmeni=(event,element)=>{
        event.preventDefault();

        getBrutoFromNeto("brutohonorar")
        .post(element)
        .then(res=>(console.log(res.data),root.render(<IzmeniNetoHonorar username={props.username} password={props.password} uvecanje={element.uvecanje} umanjenje={element.umanjenje} 
            porezi={element.porezi} id={element.id} idBruta={res.data.id} valuta={res.data.valuta} trenutnaPlata={res.data.trenutnaPlata} />)))

        
    }

    console.log("elments : "+elements)
    const elementi=elements.map((element)=>
    <tr><td>
            {element.id}</td><td >{element.uvecanje}</td><td >
            {element.umanjenje}</td>
            <td >{ispisiPoreze(element.porezi)}</td>
            <td >
                <input type={"button"} class="btn btn-link" onClick={(event)=>findBruto(event,element)}  value={"Prikaži bruto"}></input>
            </td>
            <td>
                <input type={"button"} class="btn btn-link" onClick={(event)=>obrisi(event,element)}  value={"Obriši"}></input>
            </td>
            <td>
                <input type={"button"} class="btn btn-link" onClick={(event)=>izmeni(event,element)}  value={"Izmeni"}></input>
            </td>
            <td>
                <input type={"button"} class="btn btn-link" onClick={(event)=>dupliraj(event,element)}  value={"Dupliraj"}></input>
            </td>
            </tr>
            
        )

    console.log("map:"+elementi)

        return(
            <div>
            <div class="container text-center">
                <div class="alert alert-warning"><strong><h1>Neto honorari</h1></strong></div>
                <table class="table table-bordered">
                    <tr>
                        <td height="50"><b>ID</b></td>
                        <td><b>UVEĆANJE</b></td>
                        <td ><b>UMANJENJE</b></td>
                        <td><b>POREZI</b></td>
                        <td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>

            {elementi}</table>
            {brutoPrikaz} 
            <br/>
            <input type={"button"} onClick={(event)=>dodaj(event)} class="btn btn-warning" value={"Dodaj"}></input>
            <br/><br/></div>
            <div class="container">      
            <div class="alert alert-warning">     
                <h4>Pretraga : </h4>
            <form onSubmit={pretraga}>
            Porezi : <select multiple={true} ref={porezi} onChange={(e)=> {handleSelect(e,porezi.current.value)}} class="form-control">
                <option>&nbsp;</option>
                <option value={"POTROSNJA"}>POTROSNJA</option>
                <option value={"DOHODAK"}>DOHODAK</option>
                <option value={"DOBIT"}>DOBIT</option>
                <option value={"IMOVINA"}>IMOVINA</option>               
            </select><br/>
            Uvećanje : <select  ref={uvecanje}>
                <option>&nbsp;</option>
                <option value={"PET"}>5 %</option>
                <option value={"DESET"}>10 %</option>
                <option value={"DVADESET"}>20 %</option>
                <option value={"PEDESET"}>50 %</option>
                <option value={"SEDAMDESET"}>70 %</option>
                <option value={"STO"}>100 %</option>
            </select><br/><br/>
            Umanjenje : <select  ref={umanjenje}>
                <option>&nbsp;</option>
                <option value={"PET"}>5 %</option>
                <option value={"DESET"}>10 %</option>
                <option value={"DVADESET"}>20 %</option>
                <option value={"PEDESET"}>50 %</option>
                <option value={"SEDAMDESET"}>70 %</option>
                <option value={"STO"}>100 %</option>
            </select><br/><br/>

            <input type={"submit"} value={"Pretraži"} name="pretrazi"></input>
            </form>
            </div>
            </div>
            </div>
        )

}