<template>
  <div>
    <h3>{{ name }}</h3>
    <i
      v-if="selectionMode"
      :class="
            `far ${
              selected ? icon.checkedSquare : icon.square
            } fa-lg selection-box`
          "
    ></i>
  </div>
</template>
<script>
import { mapActions, mapState } from "vuex";

export default {
  name: "role-card",
  props: {
    roleId: String,
    description: String,
    name: String,
    selectionMode: Boolean
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