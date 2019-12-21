import Vue from "vue";
import { store } from "./store";
import { router } from "./helpers";
import App from "./App";
import BootstrapVue from "bootstrap-vue";
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";
import GlobalComponents from "./helpers/globalComponents";
import GlobalDirectives from "./helpers/globalDirectives";
import Sidebar from "./components/sidebar/index";
import i18n from "./localization/i18n";

Vue.use(BootstrapVue);
Vue.use(GlobalComponents);
Vue.use(GlobalDirectives);
Vue.use(Sidebar);

//Vue.use(VeeValidate);

new Vue({
  el: "#app",
  i18n,
  router,
  store,
  render: h => h(App)
});
