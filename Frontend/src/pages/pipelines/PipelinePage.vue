<template>
  <div >
    <h1>Pipeline {{ form.name }}</h1>
   
    <div class="row">
      <div class="col">
        <dl class="row">
            <dt class="col-sm-3">Pipeline name</dt>
            <dd class="col-sm-9">{{ form.name }}</dd>

            <dt class="col-sm-3">Tasks</dt>
            <dd class="col-sm-9">
                <ul>
                    <li v-for="item in form.tasks" :key="item.task.id">
                        {{ item.task.name }}
                    </li>
                </ul>
            </dd>   
            <dt class="col-sm-3">Forecast time, ms</dt>
            <dd class="col-sm-9">{{form.forecastTime ? form.forecastTime : '-'}}</dd>        
        </dl>
      </div>
    </div>
     <div class="row">
      <div class="col">
          <button type="button" class="btn btn-primary mx-2">Run pipeline</button>
          <button type="button" class="btn btn-secondary">Recalculate forecast time</button>
      </div>
     </div>
  </div>  
</template>

<script>
import pipelinesRepository from './pipelinesRepository';
export default {
  name: 'Pipeline',
  data () {
      return {
          form:{}
      }
  },
  mounted() {
    pipelinesRepository.getById(this.$route.params.id)
      .then(response => (this.form = response.data));
  }    
}
</script>
