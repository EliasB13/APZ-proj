import "@/assets/scss/argon.scss";
import "@/assets/scss/vendor/nucleo/css/nucleo.css";
import BaseNav from "../components/BaseNav";
import BaseInput from "../components/BaseInput";
import BaseCheckbox from "../components/BaseCheckbox";
import BaseButton from "../components/BaseButton";
import BaseDropdown from "../components/BaseDropdown";
import BaseHeader from "../components/BaseHeader";
import BaseCard from "../components/BaseCard";
import UserCard from "../components/UserCard";
import RoleCard from "../components/RoleCard";
import Card from "../components/Card";
import Modal from "../components/Modal";

export default {
  install(Vue) {
    Vue.component(BaseNav.name, BaseNav);
    Vue.component(BaseInput.name, BaseInput);
    Vue.component(BaseCheckbox.name, BaseCheckbox);
    Vue.component(BaseButton.name, BaseButton);
    Vue.component(BaseDropdown.name, BaseDropdown);
    Vue.component(BaseHeader.name, BaseHeader);
    Vue.component(BaseCard.name, BaseCard);
    Vue.component(UserCard.name, UserCard);
    Vue.component(RoleCard.name, RoleCard);
    Vue.component(Card.name, Card);
    Vue.component(Modal.name, Modal);
  }
};
