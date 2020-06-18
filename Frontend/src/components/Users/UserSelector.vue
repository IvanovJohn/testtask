<template>
    <b-form-select v-model="selected" 
     :options="options" 
      value-field="id"
      text-field="name"></b-form-select>
</template>

<script>
import usersRepository from './usersRepository';

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
        console.log(data)
        this.options = this.options.concat(data.data)
      });
    }
}
</script>