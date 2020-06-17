<template>
    <span>
        <b-button v-b-modal.modalTaskCreation>Create</b-button>

        <b-modal id="modalTaskCreation" title="Create task"  @ok="handleOk" @show="handleShow">
            <b-form ref="form" @submit.stop.prevent="handleSubmit">
                <b-form-group
                    id="input-group-1"
                    label="Task name:"
                    label-for="txtName"
                    >
                    <b-form-input
                    id="txtName"
                    v-model="form.name"
                     :state="validateState('name')"
                    type="text"
                    required
                    placeholder="Enter task name"
                    aria-describedby="txtNameErrorDescription"
                    ></b-form-input>
                    <b-form-invalid-feedback
                        id="txtNameErrorDescription"
                        >This is a required field and must be at least 3 characters.</b-form-invalid-feedback>
                </b-form-group>
            </b-form>
        </b-modal>

    </span>
</template>
<script>
  import { validationMixin } from "vuelidate";
  import { required, minLength } from "vuelidate/lib/validators";

  import tasksRepository from './tasksRepository';


  export default {
        name: 'task-create', 
        mixins: [validationMixin],   
        data() {
            return {
                 form: {
                    name: ''
                 }                    
            }
        },
        validations: {  
            form:{          
                name: {
                    required,
                    minLength: minLength(3)
                }     
            }       
        },
        methods: {
            validateState(name) {
                const { $dirty, $error } = this.$v.form[name];
                return $dirty ? !$error : null;
            },            
            resetModal() {
                this.form.name = ''; 
                this.$v.$reset();                 
            },
            handleOk(bvModalEvt) {        
                bvModalEvt.preventDefault();
                this.handleSubmit();
            },
            handleSubmit() {
                this.$v.form.$touch();
                if (this.$v.form.$anyError) {
                    return;
                }
        
                tasksRepository.add(this.form).then(() => {
                    // Hide the modal manually
                    this.$nextTick(() => {                    
                        this.$bvModal.hide('modalTaskCreation');
                        this.$emit('onTaskCreated');
                    });
                });
            },
            handleShow() {
                this.resetModal();
            }
        }
  }
</script>