import axios from '@/core/axiosWrapper';

const resource = '/tasks';
export default {
    list(){
        return axios.get(`${resource}`);
    }
}