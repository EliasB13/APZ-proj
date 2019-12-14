<template>
  <div class="card-container pointer" @click="handleClick">
    <a class="card-link">
      <article class="blog-card" :class="selected ? 'item-selected' : ''">
        <img class="post-image" :src="itemImage" />
        <div class="article-details">
          <h3 class="post-title">{{ itemName }}</h3>
          <p class="post-description">{{ itemDesc }}</p>
          <p class="post-author">
            Status:
            <span
              :class="isTaken ? 'text-danger' : 'text-success'"
              class="font-weight-700"
              >{{ isTaken ? "Taken" : "Available" }}</span
            >
          </p>
        </div>
        <i
          v-if="selectionMode"
          :class="
            `far ${
              selected ? icon.checkedSquare : icon.square
            } fa-lg selection-box`
          "
        ></i>
      </article>
    </a>
  </div>
</template>
<script>
import { mapActions, mapState } from "vuex";
export default {
  name: "base-card",
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
  props: {
    itemName: String,
    itemDesc: String,
    isTaken: Boolean,
    itemImage: {
      type: String,
      default: "img/theme/item.png"
    },
    itemId: Number,
    selectionMode: Boolean
  },
  methods: {
    ...mapActions("selectedItems", ["addSelectedItem", "removeSelectedItem"]),

    //...mapActions("businessItems", ["addSelectedItem", "removeSelectedItem"]),
    handleClick() {
      if (this.selectionMode) {
        this.selected = !this.selected;
        this.selected
          ? this.addSelectedItem(this.itemId)
          : this.removeSelectedItem(this.itemId);
      }
    }
  }
};
</script>

<style lang="scss"></style>
