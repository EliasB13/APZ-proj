import "@/assets/scss/argon.scss";
import "@/assets/scss/vendor/nucleo/css/nucleo.css";
import BaseNav from "../components/BaseNav";
import BaseInput from "../components/BaseInput";
import BaseCheckbox from "../components/BaseCheckbox";
import BaseButton from "../components/BaseButton";
import BaseDropdown from "../components/BaseDropdown";
import BaseHeader from "../components/BaseHeader";

export default {
  install(Vue) {
    Vue.component(BaseNav.name, BaseNav);
    Vue.component(BaseInput.name, BaseInput);
    Vue.component(BaseCheckbox.name, BaseCheckbox);
    Vue.component(BaseButton.name, BaseButton);
    Vue.component(BaseDropdown.name, BaseDropdown);
    Vue.component(BaseHeader.name, BaseHeader);
  }
};
