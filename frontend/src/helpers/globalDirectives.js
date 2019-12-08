import clickOutside from "../helpers/globalDirectives.js";

const GlobalDirectives = {
  install(Vue) {
    Vue.directive("click-outside", clickOutside);
  }
};

export default GlobalDirectives;
