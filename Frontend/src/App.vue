<template>
  <div id="app">
     <nav class="navbar navbar-dark sticky-top bg-dark flex-md-nowrap p-0 shadow">
        <router-link class="navbar-brand col-md-3 col-lg-2 mr-0 px-" to="/">Pipeline app</router-link>
        
        <ul class="navbar-nav px-3">
          <li class="nav-item text-nowrap">
            <a class="nav-link" href="#">Sign out</a>
          </li>
        </ul>
      </nav>
    
     <div class="container-fluid">
      <div class="row">
        <sidebar />
    
        <main role="main" class="col-md-9 ml-sm-auto col-lg-10 px-md-4">                   
         <router-view/>
        </main>
      </div>
    </div>    
  </div>
</template>

<script>
  import Sidebar from '@/components/Sidebar.vue'

  export default {
  name: 'App',
  components: {
    Sidebar
  }
}
</script>

<style>
body {
  font-size: .875rem;
}

.navbar-brand {
  padding-top: .75rem;
  padding-bottom: .75rem;
  font-size: 1rem;
  background-color: rgba(0, 0, 0, .25);
  box-shadow: inset -1px 0 0 rgba(0, 0, 0, .25);
}
</style>
