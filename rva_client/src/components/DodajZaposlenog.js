import React,{useRef} from "react";
import useForm from "../useForm";
import * as ReactDOMClient from 'react-dom/client';
import Result from "./Result.js";
import axios from 'axios'

export const BASE_URL="https://localhost:44386/";

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

    const config = {
        headers: {  Authorization: 'Bearer ' +  localStorage.getItem('token'),}
    };

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
            return axios 
                        .post(`${BASE_URL}api/zaposleni`, values, config) 
                        .then(res=>alert("Novi zaposleni je dodat bez bruto honorara")) 
                        .catch(err=>alert(err))
            
        }
        if(trenutnaPlata.current.value<0){

            alert("Plata ne može biti negativan broj");return;

        }

        
        values.trenutnaPlata=trenutnaPlata.current.value;
        values.valuta=valuta.current.value;

        
          return axios 
                    .post(`${BASE_URL}api/brutohonorar`, values, config) 
                    .then(response =>(values.brutoHonorarId=response.data.id,axios 
                        .post(`${BASE_URL}api/zaposleni`, values, config) 
                        .then(res=>alert("Novi zaposleni je dodan")) 
                        .catch(err=>alert(err)))) 
                    .catch(err=>alert(err));
       
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