import Vue from 'vue'
import VueRouter from 'vue-router'
import Pipelines from '../views/Pipelines.vue'

Vue.use(VueRouter)

  const routes = [
  {
    path: '/',
    name: 'Pipelines',
    component: Pipelines
  },
  {
    path: '/tasks',
    name: 'Tasks',   
    component: () => import(/* webpackChunkName: "tasks" */ '../views/Tasks.vue')
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
