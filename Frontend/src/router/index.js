import Vue from 'vue'
import VueRouter from 'vue-router'
import Pipelines from '../pages/pipelines/PipelinesPage.vue'
import Pipeline from '../pages/pipelines/PipelinePage.vue'
import Tasks from '../pages/tasks/TasksPage.vue'

Vue.use(VueRouter)

  const routes = [
  {
    path: '/',
    name: 'Pipelines',
    component: Pipelines
  },
  {
    path: '/pipelines/:id',
    name: 'Pipeline',
    component: Pipeline
  },
  {
    path: '/tasks',
    name: 'Tasks',   
    component: Tasks
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

router.reloadCurrentPage = function(){
  const to = router.currentRoute;
  router.replace('/fakeRoute').then(()=>{
      router.replace(to)
  });      
}

export default router
