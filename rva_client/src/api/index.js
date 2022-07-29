import axios from 'axios'

export const BASE_URL="https://localhost:44386/";

export const createAPIEndpoint = endpoint => {
    let url=BASE_URL+'api/'+endpoint+'/login';
    return{
        fetch:()=> axios.get(url),
        fetchById:id=>axios.get(url+id),
        post: newRecord=>axios.post
        (url,newRecord),
        
    }
}

export const createAPIEndpointRegister = endpoint => {
    let url=BASE_URL+'api/'+endpoint+'/register';
    return{
        fetch:()=> axios.get(url),
        fetchById:id=>axios.get(url+id),
        post: newRecord=>axios.post
        (url,newRecord),
        
    }
}
