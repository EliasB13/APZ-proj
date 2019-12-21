import clickOutside from "../helpers/dropDown-click-outside.js";

const GlobalDirectives = {
  install(Vue) {
    Vue.directive("click-outside", clickOutside);
  }
};

export default GlobalDirectives;
