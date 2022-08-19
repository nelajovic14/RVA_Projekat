import React from "react";
import {useState} from "react";
import { getBrutoFromNeto,getNetos,obrisiNeto } from "../api/index.js";
import DodajNetoHonorar from "./DodajNetoHonorar"
import * as ReactDOMClient from 'react-dom/client';

export default function Neto(props){

    const [elements, setElement] = useState([]);
    const [brutoElement, setBruto]=useState(null)
    const [isChanged,setIsChanged]=useState(true)

    const dodaj=e=>{
        e.preventDefault();
        const container = document.getElementById('root');
        const root = ReactDOMClient.createRoot(container);
        root.render(<DodajNetoHonorar username={props.username} password={props.password}/>)
    }

    const findBruto=(event,element)=>{
       
        event.preventDefault();
        getBrutoFromNeto("brutohonorar")
        .post(element)
        .then(res=>(console.log(res.data),setBruto(res.data)))
        
    }

    const obrisi=(event,element)=>{
       
        event.preventDefault();
        obrisiNeto("netohonorar")
        .post(element)
        .then(setIsChanged(true),getNetos('netohonorar')
        .post()
        .then(response => (console.log(response.data),setElement(response.data),setIsChanged(true))))
    }

    var brutoPrikaz=<div></div>;

    if(brutoElement!=null){
        brutoPrikaz=<div>Id : {brutoElement.id} <br/> Trenutna plata : {brutoElement.trenutnaPlata} &nbsp; {brutoElement.valuta}</div>
    }

    const ispisiPoreze=(porezi)=>{
        const ispis=porezi.map(
            (el)=><div>{el},</div>
        )
        return ispis;
    }

    if(isChanged){
        
        getNetos('netohonorar')
        .post()
        .then(response => (setIsChanged(false),console.log(response.data[0]),setElement(response.data)))
        console.log("ele:"+isChanged)
    }
    console.log("elments : "+elements)
    const elementi=elements.map((element)=>
    <tr style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}><td style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>
            {element.id}</td><td style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>{element.uvecanje}</td><td style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>
            {element.umanjenje}</td>
            <td style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>{ispisiPoreze(element.porezi)}</td>
            <td style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>
                <input type={"button"} onClick={(event)=>findBruto(event,element)}  value={"Prikaži bruto"}></input>
            </td>
            <td style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>
                <input type={"button"} onClick={(event)=>obrisi(event,element)}  value={"Obriši"}></input>
            </td>
            </tr>
            
        )

    console.log("map:"+elementi)

        return(
            <div>
                <h3>Neto honorari</h3>
                <table style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>
                    <tr style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>
                        <td style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>Id</td>
                        <td style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>uvecanje</td>
                        <td style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>umanjenje</td>
                        <td style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>porezi</td>
                        <td>&nbsp;</td><td>&nbsp;</td></tr>
            {elementi}</table>
            <br/>
            <input type={"button"} onClick={(event)=>dodaj(event)}  value={"Dodaj"}></input>
            <br/>
            <br/>
            {brutoPrikaz}
            <br/>
            
            </div>
        )

}