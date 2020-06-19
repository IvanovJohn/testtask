<template>
    <span>
        <b-button v-b-modal.modalTaskCreation v-show="showCreateButton">Create</b-button>

        <b-modal id="modalTaskCreation" title="Create task"  @ok="handleOk" @show="handleShow">
            <b-form ref="form" @submit.stop.prevent="handleSubmit">
                <b-form-group
                    id="input-group-1"
                    label="Task name:"
                    label-for="txtName"
                    >
                    <b-form-input
                    id="txtName"
                    v-model="form.Name"
                     :state="validateState('Name')"
                    type="text"
                    autocomplete="off"                    
                    placeholder="Enter task name"
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
            </b-form>
        </b-modal>

    </span>
</template>
<script>
  import { validationMixin } from "vuelidate";
  import { required, minLength } from "vuelidate/lib/validators";
  import authService from '@/core/authService';
  import tasksRepository from './tasksRepository';  
  import serverValidationMixin from '@/mixins/serverValidation'

  export default {
        name: 'task-create', 
        mixins: [validationMixin, serverValidationMixin],   
        data() {
            return {                 
                 form: {
                    Name: ''                    
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
        
                tasksRepository.add(this.form).then(() => {
                    // Hide the modal manually
                    this.$nextTick(() => {                    
                        this.$bvModal.hide('modalTaskCreation');
                        this.$bvToast.toast("Task created", {
                            title: 'Info',           
                            variant: 'success',
                            toaster: 'b-toaster-top-center'
                        });               
                        this.$emit('onTaskCreated');
                    });
                }).catch(error => {                    
                    if (error.response.status == 400) {
                        this.handleServerErrors(error.response.data.errors)                          
                    }
                });
            },
            handleShow() {
                this.resetModal();
            }            
        }
  }
</script>