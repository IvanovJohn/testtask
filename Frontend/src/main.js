import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap-vue/dist/bootstrap-vue.css"

import Vue from 'vue'
import App from './App.vue'

import { BootstrapVue, IconsPlugin  } from 'bootstrap-vue'
Vue.use(BootstrapVue,  {
  BToast: { autoHideDelay: 3000 } 
})
Vue.use(IconsPlugin)

import router from './router'
import axiosWrapper from './core/axiosWrapper'

var vueInstance=new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
axiosWrapper.init(vueInstance);