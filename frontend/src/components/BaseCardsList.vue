<template>
  <b-row>
    <b-col xl="4" lg="6" class="mb-5" v-for="item in itemsList" :key="item.id">
      <base-card
        :itemName="item.name"
        :itemDesc="item.description"
        :isTaken="item.isTaken"
        :itemId="item.id"
        :selectionMode="selectionMode"
      ></base-card>
    </b-col>
  </b-row>
</template>

<script>
import baseCard from "./BaseCard";

export default {
  name: "base-cards-list",
  components: {
    baseCard
  },
  props: {
    itemsList: Array,
    selectionMode: Boolean
  }
};
</script>
