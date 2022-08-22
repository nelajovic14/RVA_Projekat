import React,{useRef} from "react";
import { dodajZaposlenog,dodajBruto } from "../api/index.js";
import useForm from "../useForm";
import * as ReactDOMClient from 'react-dom/client';
import Result from "./Result.js";

const getFreshModelObject=()=>({
    ime:'',
    godineIskustva:0,
    brutoHonorarId:0,
    trenutnaPlata:0,
    valuta:'',
    korisnik:''
})



export default function DodajZaposlenog(props){
    const {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChanges
    } = useForm(getFreshModelObject)



    const ime=useRef();
    const godine=useRef();
    const trenutnaPlata=useRef();
    const valuta=useRef();

    const dodaj = e=>
    {        
        e.preventDefault();
        values.korisnik=props.username;
        if(!ime.current.value){
            alert("Morate uneti ime")
            return;
        }
        if(godine.current.value<0){
            alert("Godine iskustva ne mogu biti negativan broj");
            return;
        }

        values.ime=ime.current.value;
        values.godineIskustva=godine.current.value;

        if(!trenutnaPlata.current.value){
            dodajZaposlenog('zaposleni')
            .post(values)
            .then(alert("Novi zaposleni je dodat bez bruto honorara"))
            .catch(err=>console.log(err))
            return;
        }
        if(trenutnaPlata.current.value<0){

            alert("Plata ne može biti negativan broj");return;

        }

        
        values.trenutnaPlata=trenutnaPlata.current.value;
        values.valuta=valuta.current.value;


        dodajBruto('brutohonorar')
        .post(values)
        .then(res=>(values.brutoHonorarId=res.data.id, dodajZaposlenog('zaposleni')
        .post(values)
        .then(alert("Novi zaposleni je dodat"))
        .catch(err=>console.log(err))))
        .catch(err=>console.log(err))

       
    }

    const nazad =e=>{
        e.preventDefault();
        
        const container = document.getElementById('root');
        const root = ReactDOMClient.createRoot(container);
        root.render(<Result username={props.username} password={props.password}/>)
    }

    return(
    <div class="container text-center">
            <div class="alert alert-success"><h3><strong>Dodaj novog zaposlenog : </strong></h3></div><br/>
        <form onSubmit={dodaj}> 
            Ime : <input type={"text"} name='ime' ref={ime}  ></input><br/><br/>
            Godine iskustva : <input type={"number"} name="godine" ref={godine}></input><br/><br/>
            <br/> <div class="alert alert-success"><h5>Dodaj bruto zaposlenog : </h5></div>
            Trenutna plata : <input type={"number"} name='plata' ref={trenutnaPlata}  ></input><br/><br/>
            
            Valuta: <select ref={valuta}>
            <option value={"RSD"}>RSD</option>
            <option value={"EUR"}>EUR</option>
            <option value={"KM"}>KM</option>
            </select> <br/><br/>
            <input type={"submit"} name='dodaj' value={"Dodaj"} class="btn btn-success"></input>&nbsp;&nbsp;
        <input type={"button"} name='back' value={"Nazad"} onClick={nazad} class="btn btn-success"></input><br/>
        </form>
        <br/>
    </div>
    )
}