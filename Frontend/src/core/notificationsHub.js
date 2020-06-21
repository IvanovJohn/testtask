import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr'
import config from '../config';

var vueRef = null;
const connection = new HubConnectionBuilder()
        .withUrl(`${config.apiUrl}/notifications`)        
        .configureLogging(LogLevel.Information)
        .build()

connection.on('Notification', (message) => {
    vueRef.$bvToast.toast(message, {
        title: 'Information',           
        variant: 'primary',
        toaster: 'b-toaster-top-center'
    });  
})
connection.start()

export default {
    init(vue) {
        vueRef = vue;       
    },
}
