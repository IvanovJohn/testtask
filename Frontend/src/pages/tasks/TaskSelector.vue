<template>       
    <vue-bootstrap-typeahead
        :data="tasks"
        ref="searchInput"
        v-model="searchString"            
        :serializer="s => s.name"
        placeholder="Type an task name..."
        @hit="handleTaskSelected"
    />    
</template>

<script>
    import VueBootstrapTypeahead from 'vue-bootstrap-typeahead'
    import tasksRepository from './tasksRepository';
    import _ from 'underscore'

    export default {
        name: 'task-selector', 
        components: {
            VueBootstrapTypeahead
        },
        data() {
            return {
                tasks: [],
                searchString: ''                
            }
        },
        
        methods: {
            async getTasks(query) {
                const res = await tasksRepository.list(query)                          
                this.tasks = res.data
            }, 
            handleTaskSelected(task){
                this.$emit('onTaskSelected', task)
                this.$refs.searchInput.inputValue = ''
            }
        },
        
        watch: {
            searchString: _.debounce(function(search) { this.getTasks(search) }, 500)
        }
    }
</script>