import React,{useRef} from "react";
import useForm from "../useForm";
import * as ReactDOMClient from 'react-dom/client';
import Result from "./Result.js";
import axios from 'axios'

const BASE_URL="https://localhost:44386/";

const getFreshModelObject=()=>({
    trenutnaPlata:0,
    valuta:'',
    korisnik:''
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


    const nazad =e=>{
        e.preventDefault();
        
        const container = document.getElementById('root');
        const root = ReactDOMClient.createRoot(container);
        root.render(<Result username={props.username} password={props.password}/>)
    }
    


    const dodaj = e=>
    {        
        e.preventDefault();
        values.korisnik=props.username;
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

          const config = {
            headers: {  Authorization: 'Bearer ' +  localStorage.getItem('token'),}
        };
          return axios 
                    .post(`${BASE_URL}api/brutohonorar`, values, config) 
                    .then(response =>(alert("Bruto honorar je dodat"))) 
                    .catch(err=>alert(err)); 

    }
    return(
        <div class="container text-center">
           <div class="alert alert-success"> <h3>Dodaj novi bruto honorar :</h3> </div><br/><br/>
        <form onSubmit={dodaj}> 
            Trenutna plata : <input type={"number"} name='plata' ref={trenutnaPlata} ></input><br/><br/>
            
            Valuta: <select ref={valuta}>
            <option value={"RSD"}>RSD</option>
            <option value={"EUR"}>EUR</option>
            <option value={"KM"}>KM</option>
            </select> <br/><br/><br/>
            
            <input type={"submit"} name='dodaj' value={"Dodaj"} class="btn btn-success"></input> &nbsp;&nbsp;
             <input type={"button"} name='back' value={"Nazad"} onClick={nazad} class="btn btn-success"></input>
        </form>
        
    </div>
    )
    
    
}