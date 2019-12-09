<template>
  <div class="card-container">
    <a class="card-link" href="#">
      <article class="blog-card">
        <img class="post-image" :src="itemImage" />
        <div class="article-details">
          <h3 class="post-title">{{ itemName }}</h3>
          <p class="post-description">{{ itemDesc }}</p>
          <p class="post-author">
            Status
            <span class="text-success font-weight-700">{{ isTaken }}</span>
          </p>
        </div>
      </article>
    </a>
  </div>
</template>
<script>
export default {
  name: "base-card",
  props: {
    itemName: {
      type: String,
      default: "Default item name",
      description: "Item name"
    },
    itemDesc: {
      type: String,
      default: "Default item description",
      description: "Item description"
    },
    isTaken: {
      type: Boolean,
      default: false,
      description: "Whether item taken now"
    },
    itemImage: {
      type: String,
      default: "img/theme/item.png",
      description: "Link to item image"
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
  font-family: "Roboto", sans-serif;
  font-weight: 400;
  color: $text;
  font-size: 0.9375rem;
  margin: 0;
  line-height: 1.6;
}

.blog-card {
  display: flex;
  flex-direction: row;
  background: $white;
  box-shadow: 0 0.1875rem 1.5rem $shadow;
  border-radius: 0.375rem;
  overflow: hidden;
}

.card-link {
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
