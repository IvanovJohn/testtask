<template>
    <nav id="sidebarMenu" class="col-md-3 col-lg-2 d-md-block bg-light sidebar">
        <div class="sidebar-sticky pt-3">
          <ul class="nav flex-column">
              <li class="nav-item">
                <router-link class="nav-link" to="/" exact>
                  <b-icon icon="list-nested"></b-icon>
                  Pipelines 
                </router-link>
              </li>
              <li class="nav-item">
                <router-link class="nav-link" to="/tasks" exact>
                  <b-icon icon="list-task"></b-icon>
                  Tasks
                </router-link>
              </li>
            </ul>
          </div>
    </nav>
</template>

<script>
export default {
  name: 'Sidebar',
  props: {    
  }
}
</script>

<style >
.sidebar {
  position: fixed;
  top: 0;
  bottom: 0;
  left: 0;
  z-index: 100; /* Behind the navbar */ 
  box-shadow: inset -1px 0 0 rgba(0, 0, 0, .1);
  text-align:left;
}

@media (max-width: 767.98px) {
  .sidebar {
    top: 5rem;
  }
}

.sidebar-sticky {
  position: relative;
  top: 0;
  height: calc(100vh - 48px);
  padding-top: .5rem;
  overflow-x: hidden;
  overflow-y: auto; /* Scrollable contents if viewport is shorter than content. */
}

@supports ((position: -webkit-sticky) or (position: sticky)) {
  .sidebar-sticky {
    position: -webkit-sticky;
    position: sticky;
  }
}

.sidebar .nav-link {
  font-weight: 500;
  color: #333;
}

.sidebar .nav-link .feather {
  margin-right: 4px;
  color: #999;
}

.sidebar .nav-link.router-link-active {
  color: #007bff;
}

.sidebar .nav-link:hover .feather,
.sidebar .nav-link.router-link-active .feather {
  color: inherit;
}

.sidebar-heading {
  font-size: .75rem;
  text-transform: uppercase;
}

</style>

      