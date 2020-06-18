<template>
    <b-form-select v-model="selected" 
     :options="options" 
      value-field="name"
      text-field="name"
      ref="userSelect"
      change="onChange"></b-form-select>
</template>

<script>
import usersRepository from './usersRepository';
import authService from './authService';

export default {
    name: 'UserSelector', 
    data() {
      return {
        selected: null,
        options: [
          { id: null, name: 'Please select user' }          
        ]
      }
    },
    mounted() {
      usersRepository.list().then((data) => {        
        this.options = this.options.concat(data.data)
      });
    },
    watch: {    
      selected: function (newValue) {
        if(newValue === this.options[0].name){
          newValue = null;
        }

        authService.login(newValue, null);
      }
    }
}
</script>