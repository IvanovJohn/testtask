import axios from 'axios';
import config from '../config';

let vueRef=null;

function errorResponseHandler(error) {    
    if (error.response) {        
        vueRef.$bvToast.toast(error.response.data.error.message, {
            title: 'Error',           
            variant: 'danger',
            toaster: 'b-toaster-top-center'
        });               
    }
    return Promise.reject(error);
}

let axiosInstance = axios.create({baseURL: config.apiUrl})
axiosInstance.interceptors.response.use(
    response => response,
    errorResponseHandler
 )
 axiosInstance.init = function(vue)
 {
    vueRef=vue;
 }

export default axiosInstance
    