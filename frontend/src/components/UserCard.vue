<template>
  <div
    class="card card-profile pointer"
    style="box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15);"
    @click="handleClick"
    :class="selected ? 'item-selected-moved' : ''"
  >
    <div class="row justify-content-center">
      <div class="col-lg-3 order-lg-2">
        <div class="card-profile-image">
          <img :src="image" class="rounded-circle" />
        </div>
      </div>
    </div>
    <div
      class="card-header text-center border-0 pt-8 pt-md-4 pb-0 pb-md-4"
    ></div>
    <div class="card-body pt-0 pt-md-4">
      <div class="text-center mt-6">
        <h3>{{ firstName + " " + lastName }}</h3>
        <div class="h5 font-weight-300">{{ login }}</div>
        <hr v-if="role" class="my-4" />
        <div v-if="role" class="h5 mt-4">{{ role }}</div>
      </div>
    </div>
    <i
      v-if="selectionMode"
      :class="
        `far ${selected ? icon.checkedSquare : icon.square} fa-lg selection-box`
      "
    ></i>
  </div>
</template>
<script>
import { mapActions, mapState } from "vuex";

export default {
  name: "user-card",
  props: {
    image: {
      type: String,
      default: "img/theme/team-4-800x800.jpg"
    },
    firstName: String,
    lastName: String,
    login: String,
    role: String,
    selectionMode: Boolean,
    emplId: Number
  },
  computed: {
    ...mapState({
      isSelectionReseted: state => state.selectedItems.isSelectedItemsReseted
    })
  },
  watch: {
    isSelectionReseted(newV, oldV) {
      if (newV) this.selected = false;
    }
  },
  data() {
    return {
      icon: {
        checkedSquare: "fa-check-square",
        square: "fa-square"
      },
      selected: false
    };
  },
  methods: {
    ...mapActions("selectedItems", ["addSelectedItem", "removeSelectedItem"]),

    handleClick() {
      if (this.selectionMode) {
        this.selected = !this.selected;
        this.selected
          ? this.addSelectedItem(this.emplId)
          : this.removeSelectedItem(this.emplId);
      }
    }
  }
};
</script>
