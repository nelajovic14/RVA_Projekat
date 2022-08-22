import React from "react";
import {useState} from "react";
import axios from 'axios'
import { obrisiBruto } from "../api/index.js";
import DodajBrutoHonorar from "./DodajBrutoHonorar.js";
import IzmeniBrutoHonorar from "./IzmeniBruto.js";
import * as ReactDOMClient from 'react-dom/client';

const BASE_URL="https://localhost:44386/";


export default function Bruto(props){
    const [elements, setElements] = useState([]);
    const [isChanged,setIsChanged]=useState(true);

    const container = document.getElementById('root');
    const root = ReactDOMClient.createRoot(container);

    const edit=(event,element)=>{
        event.preventDefault();

        root.render(<IzmeniBrutoHonorar id={element.id} username={props.username} password={props.password} trenutnaPlata={element.trenutnaPlata} valuta={element.valuta}/>)
    }

    const dodaj=e=>{
        e.preventDefault();
        root.render(<DodajBrutoHonorar  username={props.username} password={props.password}/>)
    }
   
    const obrisi=(event,element)=>{
       element.korisnik=props.username;
        event.preventDefault();
        console.log("obrisi")
        obrisiBruto("brutohonorar")
        .post(element)
        .then(setIsChanged(true), axios.get(BASE_URL+'api/brutohonorar')
        .then(response => (setElements(response.data),setIsChanged(true))))
        
    }

    if(isChanged){
        console.log(isChanged)
        axios.get(BASE_URL+'api/brutohonorar')
        .then(response => (setElements(response.data),setIsChanged(false)))
    }


    console.log("elements:"+elements)
    const elementi=elements.map((element)=>
    <tr>
        <td>{element.id}</td>
        <td >{element.trenutnaPlata}</td>
        <td >{element.valuta}</td>
        <td><input type={"button"}  class="btn btn-link" onClick={(event)=>obrisi(event,element)}  value={"Obriši"}></input></td>
        <td ><input type={"button"} class="btn btn-link" onClick={(event)=>edit(event,element)}  value={"Izmeni"}></input></td>
        </tr>
        
    )
        return(
            <div class="container text-center">
                
                <div class="alert alert-info"><h1><strong>Bruto honorari</strong></h1><br/><br/>  </div>
                <table class="table"><thead> 
                <tr>
                    <td ><b>ID</b></td>
                    <td ><b>TRENUTNA PLATA</b></td>
                    <td ><b>VALUTA</b></td><td>&nbsp;</td><td>&nbsp;</td></tr></thead>
                {elementi}
            </table><br/>
            <input type={"button"} onClick={(event)=>dodaj(event)}  value={"Dodaj"} class="btn btn-info"></input>
            </div>
        )
            
        
}