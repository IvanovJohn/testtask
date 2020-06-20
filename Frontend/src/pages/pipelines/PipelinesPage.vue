<template>
  <div >
    <h1>Pipelines</h1>
    <div class="row">
      <div class="col mb-2 text-right">
        <pipeline-create v-on:onPipelineCreated="refreshList"/>
      </div>
    </div>
    <div class="row">
      <div class="col">
        <pipeline-list ref="pipelineList"/>
      </div>
    </div>
  </div>  
</template>

<script>

import PipelineList from './PipelineList'
import PipelineCreate from './PipelineCreate'

export default {
  name: 'Pipelines',
  components: {
    PipelineList,
    PipelineCreate
  },
  methods: {
    refreshList(){
      this.$refs.pipelineList.refresh();
    }
  }
}
</script>
