import axios from '@/core/axiosWrapper';
import router from '@/router';

export default {
    login(user, password){
        delete axios.defaults.headers.common['Authorization'];
        
        if(user)
        {
            axios.post("token", { user, password }).then((data) => {            
                axios.defaults.headers.common['Authorization'] = "Bearer " + data.data.token
                router.reloadCurrentPage();              
            });
        } else{
            router.reloadCurrentPage();
        }
    }
}