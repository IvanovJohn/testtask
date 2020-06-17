<template>
  <div >
    <h1>Tasks</h1>
    <div class="row">
      <div class="col mb-2 text-right">
        <task-create />
      </div>
    </div>
    <div class="row">
      <div class="col">
        <task-list />
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
  }
}
</script>
