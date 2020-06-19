import { merge } from "lodash"

export default {
    data () {
        return {
            form: {                
                serverErrors:{},
            },
            serverValidation: {            
            }
        }
    },
    computed: {            
        rules() {
            return merge({}, this.serverValidation, this.clientValidation);
        }
    },    
    validations() {
        return this.rules;
    },      
    methods: {
        validateState(name) {
            const { $dirty, $error } = this.$v.form[name];
            return $dirty ? !$error : null;                              
        },
        handleServerErrors(errors){
            merge(this.form.serverErrors, errors);                                            
            var validators = { form:{}}
            for(var key in errors) {
                validators.form[key] =  {
                    serverError: ()=> { return false }
                }
            }
            this.serverValidation = validators
        },
        clearServerErrors: function() {                
            this.serverValidation.form = {};                              
        },
        clearServerError: function(model, fieldName) {                
            if (Object.prototype.hasOwnProperty.call(model, "serverErrors")) {
                if (Object.prototype.hasOwnProperty.call(model.serverErrors, fieldName)) {
                    delete model.serverErrors[fieldName];
                }
            }                
            delete this.serverValidation.form[fieldName];
            // hack to recompute rules                
            var k = this.serverValidation;
            this.serverValidation = {};
            this.serverValidation = k;                
        }
    }
 }