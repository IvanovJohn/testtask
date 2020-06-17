<template>
  <div >
    <h1>Tasks</h1>
    <task-list />
  </div>  
</template>

<script>

import TaskList from './TaskList'

export default {
  name: 'Tasks',
  components: {
    TaskList
  }
}
</script>
