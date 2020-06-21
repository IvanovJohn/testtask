import axios from 'axios';
import config from '../config';

var vueRef = null;

function errorResponseHandler(error) {
    if (error.response) {  
        if(error.response.data.error){
            vueRef.$bvToast.toast(error.response.data.error.message, {
                title: 'Error',           
                variant: 'danger',
                toaster: 'b-toaster-top-center'
            });               
        }
    }
    return Promise.reject(error);
}

export default {
    getInstance() {
        var instance = axios.create({baseURL: config.apiUrl});
        instance.interceptors.response.use(
            response => response,
            errorResponseHandler
        )
        return instance
    },
    init(vue) {
        vueRef = vue;       
    },
    setAuthHeader(token){        
        if(token){
            axios.defaults.headers.common['Authorization'] = "Bearer " + token
        }else{
            delete axios.defaults.headers.common['Authorization'];
        }
    }
}
    