import React ,{createContext,useState} from 'react'

export const stateContext=createContext();

const getFreshContext=()=>{
    return{
        tokenString:''
    }
}

export default function ContextProvider({children}){
    const [context,setContext]=useState()
    return(
        <stateProvider value={context,setContext}>
            {children}
        </stateProvider>
    )
}
