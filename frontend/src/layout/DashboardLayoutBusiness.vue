<template>
  <div class="wrapper" :class="{ 'nav-open': $sidebar.showSidebar }">
    <side-bar
      :background-color="sidebarBackground"
      short-title="GetIt"
      title="GetIt"
    >
      <template slot="links">
        <sidebar-item
          :link="{
            name: 'Items',
            icon: 'ni ni-app text-primary',
            path: '/business-items'
          }"
        />

        <sidebar-item
          :link="{
            name: 'Consumers',
            icon: 'ni ni-badge text-blue',
            path: '/consumers'
          }"
        />
        <sidebar-item
          :link="{
            name: 'Roles',
            icon: 'ni ni-ungroup text-orange',
            path: '/roles'
          }"
        />
        <sidebar-item
          :link="{
            name: 'Profile',
            icon: 'ni ni-building text-yellow',
            path: '/business-profile'
          }"
        />
      </template>
    </side-bar>
    <div class="main-content" :data="sidebarBackground">
      <dashboard-navbar></dashboard-navbar>

      <div @click="toggleSidebar">
        <fade-transition :duration="200" origin="center top" mode="out-in">
          <!-- your content here -->
          <router-view></router-view>
        </fade-transition>
        <content-footer v-if="!$route.meta.hideFooter"></content-footer>
      </div>
    </div>
  </div>
</template>
<script>
import DashboardNavbar from "./DashboardNavbar.vue";
import ContentFooter from "./ContentFooter.vue";
import { FadeTransition } from "vue2-transitions";

export default {
  components: {
    DashboardNavbar,
    ContentFooter,
    FadeTransition
  },
  data() {
    return {
      sidebarBackground: "vue"
    };
  },
  methods: {
    toggleSidebar() {
      if (this.$sidebar.showSidebar) {
        this.$sidebar.displaySidebar(false);
      }
    }
  }
};
</script>
<style lang="scss"></style>
