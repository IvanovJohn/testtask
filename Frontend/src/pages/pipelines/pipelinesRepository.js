import axios from '@/core/axiosWrapper';

const resource = '/pipelines';
export default {
    list(){
        return axios.getInstance().get(`${resource}`);
    },
    add(task){
        return axios.getInstance().post(`${resource}`, task);
    },
    delete(id){
        return axios.getInstance().delete(`${resource}/${id}`);
    }
}