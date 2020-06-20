import axios from '@/core/axiosWrapper';

const resource = '/pipelines';
export default {
    list(){
        return axios.getInstance().get(`${resource}`);
    },
    getById(id){
        return axios.getInstance().get(`${resource}/${id}`);
    },
    add(task){
        return axios.getInstance().post(`${resource}`, task);
    },
    delete(id){
        return axios.getInstance().delete(`${resource}/${id}`);
    }
}