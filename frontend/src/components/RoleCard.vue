<template>
  <b-row class="mb-3 card-hover" @click="handleClick">
    <b-col>
      <div class="card p-2 px-4" style="box-shadow: 0rem 0.1875rem 1.5rem rgba(0, 0, 0, 0.15);">
        <b-row>
          <b-col cols="auto" v-if="selectionMode" align-self="center">
            <i
              v-if="selectionMode"
              :class="
            `far ${
              selected ? icon.checkedSquare : icon.square
            } fa-lg`
          "
            ></i>
          </b-col>
          <b-col class="auto" align-self="center">
            <h3 class="mt-2">{{ name }}</h3>
          </b-col>
          <b-col align-self="center" class="text-right">
            <i class="fas fa-chevron-down"></i>
          </b-col>
        </b-row>
      </div>
      <b-collapse :id="`collapse-${roleId}`" v-model="collapseVisible" class="mt-2">
        <b-card>
          <p class="card-text">Description: {{ description }}</p>
        </b-card>
      </b-collapse>
    </b-col>
  </b-row>
</template>
<script>
import { mapActions, mapState } from "vuex";

export default {
  name: "role-card",
  props: {
    roleId: Number,
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
      selected: false,
      collapseVisible: false
    };
  },
  methods: {
    ...mapActions("selectedItems", ["addSelectedItem", "removeSelectedItem"]),

    handleClick() {
      if (this.selectionMode) {
        this.selected = !this.selected;
        this.selected
          ? this.addSelectedItem(this.roleId)
          : this.removeSelectedItem(this.roleId);
      } else {
        this.collapseVisible = !this.collapseVisible;
      }
    }
  }
};
</script>