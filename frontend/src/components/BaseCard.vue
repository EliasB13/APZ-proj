<template>
  <div class="card-container" @click="handleClick">
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
      isSelectionReseted: state => state.businessItems.isSelectedItemsReseted
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
    ...mapActions("businessItems", ["addSelectedItem", "removeSelectedItem"]),
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

<style lang="scss">
@import url("https://fonts.googleapis.com/css?family=Roboto:400,700");

$text: #777;
$black: #121212;
$white: #fff;
$red: #e04f62;
$border: #ebebeb;
$shadow: rgba(0, 0, 0, 0.2);

@mixin transition($args...) {
  transition: $args;
}

.item-selected {
  box-shadow: 3rem 3rem 2rem $shadow !important;
}

.selection-box {
  position: absolute;
  top: 10px;
  right: 10px;
}

* {
  box-sizing: border-box;
  &::before,
  &::after {
    box-sizing: border-box;
  }
}

.post-description {
  min-height: 4rem;
}

.card-container {
  height: 100%;
  font-family: "Roboto", sans-serif;
  font-weight: 400;
  color: $text;
  font-size: 0.9375rem;
  margin: 0;
  line-height: 1.6;
}

.blog-card {
  height: 100%;
  display: flex;
  flex-direction: row;
  background: $white;
  box-shadow: 0 0rem 1rem $shadow;
  border-radius: 0.375rem;
  overflow: hidden;
}

.card-link {
  height: 100%;
  position: relative;
  display: block;
  color: inherit;
  text-decoration: none;
  &:hover .post-title {
    @include transition(color 0.3s ease);
    color: $red;
  }
  &:hover .post-image {
    @include transition(opacity 0.3s ease);
    opacity: 0.5;
  }
}

.post-image {
  @include transition(opacity 0.3s ease);
  display: block;
  width: 100%;
  object-fit: cover;
}

.article-details {
  padding: 1.5rem;
}

.post-category {
  display: inline-block;
  text-transform: uppercase;
  font-size: 0.75rem;
  font-weight: 700;
  line-height: 1;
  letter-spacing: 0.0625rem;
  margin: 0 0 0.75rem 0;
  padding: 0 0 0.25rem 0;
  border-bottom: 0.125rem solid $border;
}

.post-title {
  @include transition(color 0.3s ease);
  font-size: 1.125rem;
  line-height: 1.4;
  color: $black;
  font-weight: 700;
  margin: 0 0 0.5rem 0;
}

.post-author {
  font-size: 0.875rem;
  line-height: 1;
  margin: 1.125rem 0 0 0;
  padding: 1.125rem 0 0 0;
  border-top: 0.0625rem solid $border;
}

.blog-card {
  flex-wrap: wrap;
}

.card-container {
  grid-area: main;
  align-self: center;
  justify-self: center;
}

.post-image {
  height: 100%;
}

.blog-card {
  display: grid;
  grid-template-columns: 1fr 2fr;
  grid-template-rows: 1fr;
}

@media (max-width: 40rem) {
  .blog-card {
    grid-template-columns: auto;
    grid-template-rows: 12rem 1fr;
  }
}
</style>
