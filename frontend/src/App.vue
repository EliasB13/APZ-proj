<template>
  <div class="main-div font">
    <router-view></router-view>
  </div>
</template>

<script>
import { mapState, mapActions } from "vuex";

export default {
  name: "app",
  computed: {
    ...mapState({
      alertStateChanger: state => state.alert.stateChanger,
      alert: state => state.alert
    })
  },
  methods: {
    ...mapActions({
      clearAlert: "alert/clear"
    }),
    makeToast(message, variant) {
      this.$bvToast.toast(message, {
        title: this.$t("common.notification"),
        variant: variant,
        autoHideDelay: 5000
      });
    }
  },
  watch: {
    alertStateChanger: function(newValue, oldValue) {
      if (this.alert.message) {
        this.makeToast(this.alert.message, this.alert.type);
      }
    },
    $route(to, from) {
      this.clearAlert();
    }
  }
};
</script>
