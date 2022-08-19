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

export const createAPIEndpointEdit = endpoint => {
    let url=BASE_URL+'api/'+endpoint+'/';
    return{
        fetch:()=> axios.get(url),
        fetchById:id=>axios.get(url+id),
        put: newRecord=>axios.put
        (url,newRecord),
        
    }
}

export const createAPIEndpointgetUser = endpoint => {
    let url=BASE_URL+'api/'+endpoint+'/getUser';
    return{
        fetch:()=> axios.get(url),
        fetchById:id=>axios.get(url+id),
        post: newRecord=>axios.post
        (url,newRecord),
        
    }
}

export const getBrutoFromNeto = endpoint => {
    let url=BASE_URL+'api/'+endpoint+'/getbruto';
    return{
        fetch:()=> axios.get(url),
        fetchById:id=>axios.get(url+id),
        post: newRecord=>axios.post
        (url,newRecord),
        
    }
}

export const getBrutoFromZaposleni = endpoint => {
    let url=BASE_URL+'api/'+endpoint+'/getbrutoZaposleni';
    return{
        fetch:()=> axios.get(url),
        fetchById:id=>axios.get(url+id),
        post: newRecord=>axios.post
        (url,newRecord),
        
    }
}

export const obrisiNeto = endpoint => {
    let url=BASE_URL+'api/'+endpoint+'/delete';
    return{
        fetch:()=> axios.get(url),
        fetchById:id=>axios.get(url+id),
        post: newRecord=>axios.post
        (url,newRecord),
        
    }
}

export const obrisiBruto = endpoint => {
    let url=BASE_URL+'api/'+endpoint+'/delete';
    return{
        fetch:()=> axios.get(url),
        fetchById:id=>axios.get(url+id),
        post: newRecord=>axios.post
        (url,newRecord),
        
    }
}

export const dodajBruto = endpoint => {
    let url=BASE_URL+'api/'+endpoint+'/dodaj';
    return{
        fetch:()=> axios.get(url),
        fetchById:id=>axios.get(url+id),
        post: newRecord=>axios.post
        (url,newRecord),
        
    }
}

export const obrisiZaposlenog = endpoint => {
    let url=BASE_URL+'api/'+endpoint+'/delete';
    return{
        fetch:()=> axios.get(url),
        fetchById:id=>axios.get(url+id),
        post: newRecord=>axios.post
        (url,newRecord),
        
    }
}

export const getNetos = endpoint => {
    let url=BASE_URL+'api/'+endpoint+'/get';
    return{
        fetch:()=> axios.get(url),
        fetchById:id=>axios.get(url+id),
        post: newRecord=>axios.post
        (url,newRecord),
        
    }
}

export const dodajZaposlenog = endpoint => {
    let url=BASE_URL+'api/'+endpoint+'/dodaj';
    return{
        fetch:()=> axios.get(url),
        fetchById:id=>axios.get(url+id),
        post: newRecord=>axios.post
        (url,newRecord),
        
    }
}

export const dodajNeto = endpoint => {
    let url=BASE_URL+'api/'+endpoint+'/dodaj';
    return{
        fetch:()=> axios.get(url),
        fetchById:id=>axios.get(url+id),
        post: newRecord=>axios.post
        (url,newRecord),
        
    }
}
