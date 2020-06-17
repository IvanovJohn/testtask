<template>
  
    <b-table
      id="tasks-table"
      ref="table"
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
      <template v-slot:cell(actions)="data">                     
        <b-button size="sm" variant="outline" class="m-0 mr-2 p-0" @click="editTask(data.item.id)" >
          <b-icon icon="pencil-square" aria-hidden="true" ></b-icon>
        </b-button>
        <b-button size="sm" variant="outline" class="m-0 p-0" @click="deleteTask(data.item.id)" >
          <b-icon icon="trash-fill" aria-hidden="true" ></b-icon>
        </b-button>         
      </template>
      <template v-slot:table-colgroup="scope">
        <col
          v-for="field in scope.fields"
          :key="field.key"
          :style="{ width: field.key === 'actions' ? '60px' : '' }"
        >
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
          },
          {
            key: 'actions',
            label: ''
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
      },
      refresh(){
        this.$refs.table.refresh();
      },
      deleteTask(id){
        alert(id)
      },
      editTask(id){
        alert(id)
      }
    }
  }
</script>