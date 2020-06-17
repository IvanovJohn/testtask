<template>
  <div >
    <h1>Tasks</h1>
    <div class="row">
      <div class="col mb-2 text-right">
        <task-create v-on:onTaskCreated="refreshList"/>
      </div>
    </div>
    <div class="row">
      <div class="col">
        <task-list ref="taskList"/>
      </div>
    </div>
  </div>  
</template>

<script>

import TaskList from './TaskList'
import TaskCreate from './TaskCreate'

export default {
  name: 'Tasks',
  components: {
    TaskList,
    TaskCreate
  },
  methods: {
    refreshList(){
      this.$refs.taskList.refresh();
    }
  }
}
</script>
