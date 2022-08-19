import React from "react";
import {useState} from "react";
import axios from 'axios'
import { obrisiBruto } from "../api/index.js";
import DodajBrutoHonorar from "./DodajBrutoHonorar.js";
import * as ReactDOMClient from 'react-dom/client';

const BASE_URL="https://localhost:44386/";


export default function Bruto(props){
    const [elements, setElements] = useState([]);
    const [isChanged,setIsChanged]=useState(true);

    const dodaj=e=>{
        e.preventDefault();
        const container = document.getElementById('root');
        const root = ReactDOMClient.createRoot(container);
        root.render(<DodajBrutoHonorar username={props.username} password={props.password}/>)
    }
   
    const obrisi=(event,element)=>{
       
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
    <tr style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>
        <td style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>{element.id}</td>
        <td style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>{element.trenutnaPlata}</td>
        <td style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>{element.valuta}</td>
        <td style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>
            <input type={"button"} onClick={(event)=>obrisi(event,element)}  value={"Obriši"}></input>
        </td>
        </tr>
        
    )

        return(
            
            <div>
                <h3>Bruto honorari</h3>
                <table style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}> 
                <tr style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>
                    <td style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>Id</td>
                    <td style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>trenutna plata</td>
                    <td style={{'borderWidth': '1px', 'borderStyle': 'solid','borderColor': 'red'}}>valuta</td></tr>
                {elementi}
            </table>
            <br/>
            <p>
            <input type={"button"} onClick={(event)=>dodaj(event)}  value={"Dodaj"}></input>
            </p>
            </div>
        )
            
        
}