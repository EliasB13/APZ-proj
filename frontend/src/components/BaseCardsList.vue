<template>
  <b-row>
    <b-col :class="'mb-5 ' + colsClasses" v-for="item in itemsList" :key="item.id">
      <base-card
        v-if="cardType == 'base-card'"
        :itemName="item.name"
        :itemDesc="item.description"
        :isTaken="item.isTaken"
        :itemId="item.id"
        :itemRfid="item.rfid"
        :selectionMode="selectionMode"
      ></base-card>
      <user-card
        class="mb-5"
        v-else-if="cardType == 'user-card'"
        :firstName="item.firstName"
        :lastName="item.lastName"
        :login="item.login"
        :role="item.role ? item.role.name : ''"
        :selectionMode="selectionMode"
        :emplId="item.employeeId"
      ></user-card>
    </b-col>
  </b-row>
</template>

<script>
export default {
  name: "base-cards-list",
  props: {
    itemsList: Array,
    selectionMode: Boolean,
    cardType: String,
    colsClasses: String
  }
};
</script>
