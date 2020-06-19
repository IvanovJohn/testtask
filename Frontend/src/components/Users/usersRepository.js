import axios from '@/core/axiosWrapper';

const resource = '/users';
export default {
    list(){
        return axios.getInstance().get(`${resource}`);
    }
}