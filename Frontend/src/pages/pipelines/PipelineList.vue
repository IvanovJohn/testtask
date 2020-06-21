<template>  
    <b-table
      id="pipelines-table"
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
      <template v-slot:cell(name)="row">  
        <router-link :to="{ name: 'Pipeline', params: { id: row.item.id }}">{{ row.item.name }}  </router-link>
                
      </template>
      <template v-slot:cell(forecastTime)="forecastTime">        
        {{forecastTime.value ? forecastTime.value : '-'}}
      </template>
      <template v-slot:cell(actions)="data">                             
        <b-button size="sm" variant="outline" class="m-0 p-0" @click="deletePipeline(data.item)"
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
  import pipelinesApi from './pipelinesApi';
  import authService from '@/core/authService';

  export default {
    name: 'pipeline-list',
    data () {
      return {
        isBusy: false,
        fields: [
          {
            key: 'name',
            label: 'Name',
          },
          {
            key: 'forecastTime',
            label: 'Forecast time, ms',
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
        let promise = pipelinesApi.list();
        return promise.then((data) => {
          const items = data.data;
          return(items);
        }).catch(() => [])
      },
      refresh(){
        this.$refs.table.refresh();
      },
      deletePipeline(pipeline){
          this.$bvModal.msgBoxConfirm('Are you sure?')
            .then((value) => {
              if(value){
                pipelinesApi.delete(pipeline.id).then(() => {
                  this.refresh();
                  this.$bvToast.toast(`Pipeline '${pipeline.name}' deleted`, {
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