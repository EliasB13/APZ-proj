<template>
  <b-row class="mb-3">
    <b-col>
      <div
        class="card p-2 px-4 role-card-hover pointer"
        style="box-shadow: 0rem 0.1875rem 1.5rem rgba(0, 0, 0, 0.15);"
        @click="roleClick(readerId)"
      >
        <b-row>
          <b-col class="auto" align-self="center">
            <h3 class="mt-2">{{ $t("readersPage.reader") + " " + readerId }}</h3>
          </b-col>
        </b-row>
      </div>
    </b-col>
  </b-row>
</template>
<script>
import { mapActions, mapState } from "vuex";

export default {
  name: "reader-card",
  props: {
    readerId: Number
  },
  computed: {},
  data() {
    return {};
  },
  methods: {
    roleClick(readerId) {
      this.$router.push({ name: "reader", params: { readerId: readerId } });
    }
  }
};
</script>
