import React,{useRef, useState} from "react";
import { dodajNeto,dodajBruto } from "../api/index.js";
import useForm from "../useForm";
import * as ReactDOMClient from 'react-dom/client';
import Result from "./Result.js";

const getFreshModelObject=()=>({
    porezi:{},
    brutoHonorarId:0,
    uvecanje:'',
    umanjenje:'',
    trenutnaPlata:0,
    valuta:''
})

export default function DodajNetoHonorar(props){
    const {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChanges
    } = useForm(getFreshModelObject)

    const porezi=useRef();
    const uvecanje=useRef();
    const umanjenje=useRef();
    const trenutnaPlata=useRef();
    const valuta=useRef();

    const dodaj = e=>
    {        
        e.preventDefault();
        if(!trenutnaPlata.current.value){
            alert("Morate uneti trenutnu platu")
            return;
        }
        if(trenutnaPlata.current.value<0){
            alert("Trenutna plata ne može biti negativan broj");
            return;
        }

        values.uvecanje=uvecanje.current.value;
        values.umanjenje=umanjenje.current.value;
        values.trenutnaPlata=trenutnaPlata.current.value;
        values.valuta=valuta.current.value;

        dodajBruto('brutohonorar')
        .post(values)
        .then(res=>(values.brutoHonorarId=res.data.id, dodajNeto('netohonorar')
            .post(values)
            .then(res=>(console.log(res,res.data), alert("Novi neto honorar je dodat")))
            .catch(err=>console.log(err))))
        .catch(err=>console.log(err))

       
    }

    const nazad =e=>{
        e.preventDefault();
        
        const container = document.getElementById('root');
        const root = ReactDOMClient.createRoot(container);
        root.render(<Result username={props.username} password={props.password}/>)
    }
    var opcije = [];
    const handleSelect = function(e,selectedItems) {
        console.log(selectedItems);
        opcije.push(selectedItems)
        values.porezi=opcije;
    }

    return(
    <div>
            
        <form onSubmit={dodaj}> 
            Porezi : <select multiple={true} ref={porezi} onChange={(e)=> {handleSelect(e,porezi.current.value)}} >
                <option value={"POTROSNJA"}>POTROSNJA</option>
                <option value={"DOHODAK"}>DOHODAK</option>
                <option value={"DOBIT"}>DOBIT</option>
                <option value={"IMOVINA"}>IMOVINA</option>
            </select><br/><br/>
            Uvećanje : <select  ref={uvecanje}>
                <option value={"PET"}>5 %</option>
                <option value={"DESET"}>10 %</option>
                <option value={"DVADESET"}>20 %</option>
                <option value={"PEDESET"}>50 %</option>
                <option value={"SEDAMDESET"}>70 %</option>
                <option value={"STO"}>100 %</option>
            </select><br/><br/>
            Umanjenje : <select  ref={umanjenje}>
                <option value={"PET"}>5 %</option>
                <option value={"DESET"}>10 %</option>
                <option value={"DVADESET"}>20 %</option>
                <option value={"PEDESET"}>50 %</option>
                <option value={"SEDAMDESET"}>70 %</option>
                <option value={"STO"}>100 %</option>
            </select><br/><br/>

            <br/><h5>Dodaj bruto honorar : </h5>
            Trenutna plata : <input type={"number"} name='plata' ref={trenutnaPlata}  ></input><br/>
            
            Valuta: <select ref={valuta}>
            <option value={"RSD"}>RSD</option>
            <option value={"EUR"}>EUR</option>
            <option value={"KM"}>KM</option>
            </select> <br/><br/>
            <input type={"submit"} name='dodaj' value={"Dodaj"} ></input><br/>
        </form>
        <br/>
        <input type={"button"} name='back' value={"Nazad"} onClick={nazad}></input><br/>
    </div>
    )
}