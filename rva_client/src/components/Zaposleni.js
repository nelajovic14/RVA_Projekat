import React from "react";
import {useState} from "react";
import axios from 'axios'
import { getBrutoFromZaposleni, obrisiZaposlenog } from "../api/index.js";
import DodajZaposlenog from "./DodajZaposlenog";
import * as ReactDOMClient from 'react-dom/client';

const BASE_URL="https://localhost:44386/";

export default function Zapolseni(props){
    const [elements, setElements] = useState([]);
    const [brutoElement, setBruto] = useState(null);
    const [isChanged,setIsChanged]=useState(true)

    const findBruto=(event,element)=>{
       
        event.preventDefault();
        getBrutoFromZaposleni("brutohonorar")
        .post(element)
        .then(res=>(console.log(res.data),setBruto(res.data)))
        
    }

    const obrisi=(event,element)=>{
       
        event.preventDefault();
        obrisiZaposlenog("zaposleni")
        .post(element)
        .then(setIsChanged(true),axios.get(BASE_URL+'api/zaposleni')
        .then(response => (console.log("response : "+response.data),setElements(response.data),setIsChanged(true))))
        
    }

    var brutoPrikaz=<div></div>;

    if(brutoElement!=null){
        brutoPrikaz=<div>Id : {brutoElement.id} <br/> Trenutna plata : {brutoElement.trenutnaPlata} &nbsp; {brutoElement.valuta}</div>
    }

    const dodaj=e=>{
        e.preventDefault();
        const container = document.getElementById('root');
        const root = ReactDOMClient.createRoot(container);
        root.render(<DodajZaposlenog username={props.username} password={props.password}/>)
    }
    
    if(isChanged){
        
    axios.get(BASE_URL+'api/zaposleni')
        .then(response => (console.log("response : "+response.data),setElements(response.data),setIsChanged(false)))

    }

        console.log("elements:"+elements)
        const elementi=elements.map((element)=>
        <tr style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>
            <td style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>{element.id}</td>
            <td style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>{element.ime}</td>
            <td style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>{element.godineIskustva}</td>
            <td style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>
                <input type={"button"} onClick={(event)=>findBruto(event,element)}  value={"Prikaži bruto"}></input></td>
            <td><input type={"button"} onClick={(event)=>obrisi(event,element)}  value={"Obriši"}></input></td>
            </tr>
        )
    
        return(
            <div>
                <h3>Zaposleni</h3>
                <table style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}> 
                <tr style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>
                    <td style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>Id</td>
                    <td style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>Ime</td>
                    <td style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>Godine iskustva</td>
                    <td>&nbsp;</td><td>&nbsp;</td>
                    </tr>
                {elementi}
            </table>
            <br/>
            <input type={"button"} onClick={(event)=>dodaj(event)}  value={"Dodaj"}></input>
            <br/>
            {brutoPrikaz}
            
            </div>

        )
}