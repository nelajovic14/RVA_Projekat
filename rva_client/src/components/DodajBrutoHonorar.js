import React,{useRef} from "react";
import { dodajBruto } from "../api/index.js";
import useForm from "../useForm";
import * as ReactDOMClient from 'react-dom/client';
import Result from "./Result.js";

const getFreshModelObject=()=>({
    trenutnaPlata:-1,
    valuta:"RSD"
})

export default function DodajBrutoHonorar(props){
    const {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChanges
    } = useForm(getFreshModelObject)

    const trenutnaPlata=useRef();
    const valuta=useRef();
    const dodaj = e=>
    {        
        e.preventDefault();
        if(!trenutnaPlata.current.value){
            alert("Morate uneti platu")
            return;
        }
        if(trenutnaPlata.current.value<0){
            alert("Plata ne može biti negativan broj");
            return;
        }

        values.trenutnaPlata=trenutnaPlata.current.value;
        values.valuta=valuta.current.value;

        dodajBruto('brutohonorar')
        .post(values)
        .then(alert("Bruto honorar je dodat"))
        .catch(err=>console.log(err))
    }

    const nazad =e=>{
        e.preventDefault();
        
        const container = document.getElementById('root');
        const root = ReactDOMClient.createRoot(container);
        root.render(<Result username={props.username} password={props.password}/>)
    }

    return(
    <div>
            
        <form onSubmit={dodaj}> 
            Trenutna plata : <input type={"number"} name='plata' ref={trenutnaPlata} ></input><br/>
            
            Valuta: <select ref={valuta}>
            <option value={"RSD"}>RSD</option>
            <option value={"EUR"}>EUR</option>
            <option value={"KM"}>KM</option>
            </select> <br/><br/>
            
            <input type={"submit"} name='dodaj' value={"Dodaj"} onChange={handleInputChanges}></input><br/>
        </form>
        <br/>
        <input type={"button"} name='back' value={"Nazad"} onClick={nazad}></input><br/>
    </div>
    )
}