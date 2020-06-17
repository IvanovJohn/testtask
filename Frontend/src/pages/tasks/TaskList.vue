<template>
  
    <b-table
      id="tasks-table"
      :busy.sync="isBusy"
      :items="itemsProvider" 
      :fields="fields" 
      show-empty
      bordered small hover head-variant="light" >
    <template v-slot:table-busy>
        <div class="text-center my-2">
          <b-spinner class="align-middle"></b-spinner>
          <strong> Loading...</strong>
        </div>
      </template>
      <template v-slot:cell(averageTime)="averageTime">        
        {{averageTime.value ? averageTime.value : '-'}}
      </template>
    </b-table>
  
</template>

<script>
  import tasksRepository from './tasksRepository';

  export default {
    name: 'task-list',
    data () {
      return {
        isBusy: false,
        fields: [
          {
            key: 'name',
            label: 'Name',
          },
          {
            key: 'averageTime',
            label: 'Average time, ms',
          }         
        ]
      }
    },
    methods: {
      itemsProvider () {
        let promise = tasksRepository.list();
        return promise.then((data) => {
          const items = data.data;
          return(items);
        }).catch(() => [])
      }
    }
  }
</script>