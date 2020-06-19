import axios from '@/core/axiosWrapper';
import router from '@/router';

export default {
    currentUser: null,
    login(user, password){                
        if(user)
        {
            axios.getInstance().post("token", { user: user.name, password }).then((data) => {   
                var token = data.data.token;
                axios.setAuthHeader(token)
                this.currentUser = user
                this.currentUser.token = token
                router.reloadCurrentPage();                                
            });
        } else {
            axios.setAuthHeader(null)
            this.currentUser = null;            
            router.reloadCurrentPage();            
        }
    }    
}