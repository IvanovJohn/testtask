import axios from '@/core/axiosWrapper';

const resource = '/tasks';
export default {
    list(searchStr){
        if(!searchStr){
            return axios.getInstance().get(`${resource}`);
        }
        return axios.getInstance().get(`${resource}?s=${searchStr}`);
    },
    add(task){
        return axios.getInstance().post(`${resource}`, task);
    },
    delete(id){
        return axios.getInstance().delete(`${resource}/${id}`);
    }
}