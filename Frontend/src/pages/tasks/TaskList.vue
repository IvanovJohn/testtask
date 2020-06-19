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
        <b-button size="sm" variant="outline" class="m-0 p-0" @click="deleteTask(data.item)"
         v-if="isDeleteButtonVisible(data.item)" >
          <b-icon icon="trash-fill" aria-hidden="true" ></b-icon>
        </b-button>         
      </template>
      <template v-slot:table-colgroup="scope">
        <col
          v-for="field in scope.fields"
          :key="field.key"
          :style="{ width: field.key === 'actions' ? '30px' : '' }"          
        >
      </template>
    </b-table>
  
</template>

<script>
  import tasksRepository from './tasksRepository';
  import authService from '@/core/authService';

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
    computed: {
       
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
      deleteTask(task){
          this.$bvModal.msgBoxConfirm('Are you sure?')
            .then((value) => {
              if(value){
                tasksRepository.delete(task.id).then(() => {
                  this.refresh();
                  this.$bvToast.toast(`Task '${task.name}' deleted`, {
                                    title: 'Info',           
                                    variant: 'success',
                                    toaster: 'b-toaster-top-center'
                                });    
                })
              }
            })            
      },
      isDeleteButtonVisible(item) {
        if(!authService.currentUser){
          return false
        }
        
        return item.creatorUserId === authService.currentUser.id
      }
    }
  }
</script>