<template>
  <div>
    <base-header type="gradient-success" class="pb-6 pb-6 pt-5 pt-md-8">
      <div class="container-fluid d-flex align-items-center">
        <div class="row">
          <div class="col-lg-7 col-md-10">
            <p class="text-white mt-0 mb-5">{{ $t("activeItemsPage.secondaryHeader") }}</p>
          </div>
        </div>
      </div>
    </base-header>

    <div class="container-fluid mt--7 mb-5"></div>

    <div class="container-fluid">
      <div v-if="showEmptyList" style="margin-top: 8rem;">
        <p>{{ $t("common.emptyList") }}</p>
      </div>
      <base-cards-list
        colsClasses="col-xl-4 col-lg-6"
        cardType="base-card"
        v-if="showItems"
        :itemsList="activeItems"
      ></base-cards-list>
    </div>

    <div id="overlay" v-if="showSpinner">
      <b-spinner class="spinner-scaled" label="loading"></b-spinner>
      <br />Loading
    </div>
  </div>
</template>

<script>
import BaseCardsList from "../../components/BaseCardsList";
import { mapState, mapActions } from "vuex";

export default {
  components: {
    BaseCardsList
  },
  data() {
    return {};
  },
  computed: {
    ...mapState({
      activeItems: state => state.privateItems.activeItems,
      status: state => state.privateItems.status
    }),
    showSpinner() {
      return this.status.activeItemsLoading;
    },
    showItems() {
      return this.status.activeItemsLoaded;
    },
    showEmptyList() {
      return this.status.activeItemsLoaded && this.activeItems.length == 0;
    }
  },
  methods: {
    ...mapActions("privateItems", ["getActiveItems"])
  },
  mounted() {
    this.getActiveItems();
  }
};
</script>
<style></style>
