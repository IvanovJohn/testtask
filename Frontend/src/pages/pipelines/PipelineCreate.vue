<template>
    <span>
        <b-button v-b-modal.modalPipelineCreation v-show="showCreateButton">Create</b-button>

        <b-modal id="modalPipelineCreation" title="Create pipeline"  @ok="handleOk" @show="handleShow">
            <b-form ref="form" @submit.stop.prevent="handleSubmit">
                <b-form-group
                    id="input-group-1"
                    label="Pipeline name:"
                    label-for="txtName"
                    >
                    <b-form-input
                    id="txtName"
                    v-model="form.Name"
                     :state="validateState('Name')"
                    type="text"
                    autocomplete="off"                    
                    placeholder="Enter pipeline name"
                    aria-describedby="txtNameErrorDescription"
                    @input="clearServerError($v.form.$model, 'Name')"
                    ></b-form-input>                 
                    <b-form-invalid-feedback
                        id="txtNameErrorDescription">
                         <div v-if="$v.form.Name.serverError !== false">
                            This is a required field and must be at least 3 characters.
                        </div>
                        <div v-if="$v.form.Name.serverError === false">
                            <p v-for="error of form.serverErrors['Name']" v-bind:Key="error">
                                {{ error }}
                            </p>
                        </div>
                    </b-form-invalid-feedback>                    
                </b-form-group>
                <b-form-group
                    id="input-group-1"
                    label="Tasks:"                    
                    >
                    <ul>
                        <li v-for="item in form.Tasks" :key="item.taskId">
                            {{ item.task.name }}                             
                            <b-button size="sm" variant="outline" class="m-0 p-0" @click="removeTask(item)">
                                <b-icon icon="x" aria-hidden="true" ></b-icon>
                            </b-button>    
                        </li>
                    </ul>
                  <task-selector v-on:onTaskSelected="taskSelected" />
                </b-form-group>
            </b-form>
        </b-modal>

    </span>
</template>
<script>
  import { validationMixin } from "vuelidate";
  import { required, minLength } from "vuelidate/lib/validators";
  import authService from '@/core/authService';
  import pipelinesApi from './pipelinesApi';  
  import serverValidationMixin from '@/mixins/serverValidation'
  import TaskSelector from '@/pages/tasks/TaskSelector'

  export default {
        name: 'pipeline-create', 
        mixins: [validationMixin, serverValidationMixin],   
        data() {
            return {                 
                 form: {
                    Name: '',
                    Tasks: []                  
                 },
                 clientValidation: {
                    form: {
                        Name: {
                           required,
                           minLength: minLength(3)
                        }                        
                    }
                 },
                 showCreateButton: authService.currentUser                    
            }
        },                  
        methods: {                     
            resetModal() {
                this.form.Name = ''; 
                this.$v.$reset();                 
            },
            handleOk(bvModalEvt) {        
                bvModalEvt.preventDefault();
                this.handleSubmit();
            },
            handleSubmit() {
                this.$v.form.$touch();
                this.clearServerErrors();
                if (this.$v.form.$anyError) {
                    return;
                }
        
                pipelinesApi.add(this.form).then(() => {
                    // Hide the modal manually
                    this.$nextTick(() => {                    
                        this.$bvModal.hide('modalPipelineCreation');
                        this.$bvToast.toast("Pipeline created", {
                            title: 'Info',           
                            variant: 'success',
                            toaster: 'b-toaster-top-center'
                        });               
                        this.$emit('onPipelineCreated');
                    });
                }).catch(error => {                    
                    if (error.response.status == 400) {
                        this.handleServerErrors(error.response.data.errors)                          
                    }
                });
            },
            handleShow() {
                this.resetModal();
            },
            taskSelected(task){  
                if(this.form.Tasks.length==0){            
                    this.form.Tasks.push({
                        taskId: task.id,
                        task: task
                    })
                }else{
                    this.form.Tasks.push({
                        taskId: task.id,
                        previosTaskId: this.form.Tasks[this.form.Tasks.length -1].taskId,
                        task: task
                    })
                }
            },
            removeTask(task){
                const index = this.form.Tasks.indexOf(task);
                if (index > -1) {
                    this.form.Tasks.splice(index, 1);
                }
            }          
        },
        components: {
            TaskSelector
        }
  }
</script>