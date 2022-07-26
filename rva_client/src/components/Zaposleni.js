import React from "react";
import {useState} from "react";
import axios from 'axios'
import { getBrutoFromZaposleni, obrisiZaposlenog } from "../api/index.js";
import DodajZaposlenog from "./DodajZaposlenog";
import * as ReactDOMClient from 'react-dom/client';
import EditZaposleni from "./EditZaposleni.js";

const BASE_URL="https://localhost:44386/";

export default function Zapolseni(props){
    const [elements, setElements] = useState([]);
    const [brutoElement, setBruto] = useState(null);
    const [isChanged,setIsChanged]=useState(true)

    const container = document.getElementById('root');
    const root = ReactDOMClient.createRoot(container);

    const findBruto=(event,element)=>{
       
        event.preventDefault();

        const config = {
            headers: {  Authorization: 'Bearer ' +  localStorage.getItem('token'),}
        };
        return axios 
            .post(`${BASE_URL}api/brutohonorar/getbrutoZaposleni`, element, config) 
            .then(response =>(setBruto(response.data))) 
            .catch(err=>alert(err)); 
    
    }

    const obrisi=(event,element)=>{
        element.korisnik=props.username;
        event.preventDefault();

        axios.delete(`${BASE_URL}api/zaposleni`, {
            headers: {
                Authorization: 'Bearer ' +  localStorage.getItem('token'),
            },
            data: {
              id:element.id,
              ime:element.ime,
              godineIskustva:element.godineIskustva,
              brutoHonorarId:element.brutoHonorarId
            }
          })
          .then(res=>(alert("obrisano"),setIsChanged(true)))
          .catch(err=>alert(err))
        
    }

    var brutoPrikaz=<div></div>;

    if(brutoElement!=null){
        brutoPrikaz=<div class="jumbotron">Id : {brutoElement.id} <br/> Trenutna plata : {brutoElement.trenutnaPlata} &nbsp; {brutoElement.valuta}</div>
    }

    const dodaj=e=>{
        e.preventDefault();
        
        root.render(<DodajZaposlenog username={props.username} password={props.password}/>)
    }

    const edit=(event,element)=>{
        event.preventDefault();
        
        const config = {
            headers: {  Authorization: 'Bearer ' +  localStorage.getItem('token'),}
        };
        return axios 
            .post(`${BASE_URL}api/brutohonorar/getbrutoZaposleni`, element, config) 
            .then(res =>(root.render(<EditZaposleni username={props.username} password={props.password} ime={element.ime} id={element.id} godineIskustva={element.godineIskustva} brutoId={res.data.id} trenutnaPlata={res.data.trenutnaPlata} valuta={res.data.valuta}/>))) 
            .catch(err=>alert(err));
    }


    const osvezi = e=>{
        e.preventDefault();
        setIsChanged(true);
    }

    if(isChanged){
        
        axios.get(`${BASE_URL}api/zaposleni`, {
            headers: {
                Authorization: 'Bearer ' +  localStorage.getItem('token'),
            }
          })
          .then(response => (setElements(response.data),setIsChanged(false)))
          .catch(err=>alert(err))


    }

        console.log("elements:"+elements)
        const elementi=elements.map((element)=>
        <tr>
            <td>{element.id}</td>
            <td>{element.ime}</td>
            <td>{element.godineIskustva}</td>
            <td>
                <input type={"button"} onClick={(event)=>findBruto(event,element)}  value={"Prikaži bruto"} class="btn btn-link"></input></td>
            <td><input type={"button"} onClick={(event)=>obrisi(event,element)}  value={"Obriši"} class="btn btn-link"></input></td>
            <td><input type={"button"} onClick={(event)=>edit(event,element)}  value={"Izmeni"} class="btn btn-link"></input></td>
            </tr>
        )
    
    return(
        <div class="container text-center">
            <div class="alert alert-danger"><h1><strong>Zaposleni</strong></h1></div>
            <div class="table-responsive">
            <table class="table"> 
            <tr>
                <td><b>ID</b></td>
                <td><b>IME</b></td>
                <td><b>GODINE ISKUSTVA</b></td>
                <td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>
                </tr>
            {elementi}
        </table></div>
        <br/>
        {brutoPrikaz} <br/><br/>
        <input type={"button"} onClick={(event)=>dodaj(event)} class="btn btn-danger" value={"Dodaj"}></input>         
        <br/><br/>
            <input type={"button"} onClick={(event)=>osvezi(event)} class="btn btn-danger" value={"Osveži"}></input>
        </div>
    )
}