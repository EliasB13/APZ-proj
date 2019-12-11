<template>
  <div class="main-div">
    <!-- <div class="row">
      <div class="col-sm-6 offset-sm-3">
        <div v-if="alert.message" :class="`alert ${alert.type}`">
          {{ alert.message }}
        </div>
      </div>
    </div> -->

    <router-view></router-view>
  </div>
</template>

<script>
import { mapState, mapActions } from "vuex";

export default {
  name: "app"
  // computed: {
  //   ...mapState({
  //     alert: state => state.alert
  //   })
  // },
  // methods: {
  //   ...mapActions({
  //     clearAlert: "alert/clear"
  //   }),
  //   makeToast(message, variant) {
  //     this.$bvToast.toast(message, {
  //       title: "Notification",
  //       variant: variant,
  //       autoHideDelay: 5000
  //     });
  //   }
  // },
  // watch: {
  //   $route(to, from) {
  //     // clear alert on location change
  //     this.clearAlert();
  //   },
  //   alert(newV, oldV) {
  //     console.log(newV);
  //     if (alert.message) {
  //       this.makeToast(this.alert.message, this.alert.type);
  //     }
  //   }
  // }
};
</script>
