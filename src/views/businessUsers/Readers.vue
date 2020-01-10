<template>
  <div>
    <base-header type="gradient-success" class="pb-6 pb-8 pt-5 pt-md-8">
      <div class="container-fluid d-flex align-items-center">
        <div class="row">
          <div class="col-lg-7 col-md-10">
            <p class="text-white mt-0 mb-5">{{ $t("readersPage.secondaryHeader") }}</p>
          </div>
        </div>
      </div>
    </base-header>

    <div class="container-fluid mt--7 mb-5">
      <b-row class="px-3">
        <b-col class="manage-bar" style="background-color: white; border-radius: 0.375rem">
          <b-row>
            <b-col align-self="center">
              <h2 class="pl-3">{{ $t("rolesPage.manageBar.manage") }}</h2>
            </b-col>
            <b-col cols="auto" class="p-3">
              <base-button
                class="float-right"
                type="success"
                icon="ni ni-fat-add"
                @click="orderReaderClick"
              >{{ $t("readersPage.orderReaderBtn") }}</base-button>
            </b-col>
          </b-row>
        </b-col>
      </b-row>
    </div>

    <div class="container-fluid">
      <div v-if="showEmptyList">
        <p>{{ $t("common.emptyList") }}</p>
      </div>
      <div v-if="showReaders">
        <reader-card v-for="reader in readers" :key="reader.readerId" :readerId="reader.readerId"></reader-card>
      </div>
    </div>

    <div id="overlay" v-if="showSpinner">
      <b-spinner class="spinner-scaled" label="loading"></b-spinner>
      <br />
      {{ $t("common.spinnerText") }}
    </div>
  </div>
</template>
<script>
import { mapState, mapActions } from "vuex";

export default {
  computed: {
    ...mapState({
      readers: state => state.readers.readers,
      status: state => state.readers.status
    }),
    showSpinner() {
      return this.status.readersLoading || this.status.readerOrdering;
    },
    showReaders() {
      return this.status.readersLoaded;
    },
    showEmptyList() {
      return this.status.readersLoaded && this.readers.length == 0;
    }
  },
  methods: {
    ...mapActions("readers", ["getReaders", "orderReader"]),
    orderReaderClick() {
      this.orderReader();
    }
  },
  mounted() {
    this.getReaders();
  }
};
</script>