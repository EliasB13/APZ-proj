<template>
  <div>
    <base-header type="gradient-success" class="pb-6 pb-6 pt-5 pt-md-8">
      <div class="container-fluid d-flex align-items-center">
        <div class="row">
          <div class="col-lg-7 col-md-10">
            <p
              class="text-white mt-0 mb-5"
            >This is your available business page. Here you can explore services shared items of which you can use</p>
          </div>
        </div>
      </div>
    </base-header>

    <div class="container-fluid mt--7 mb-5"></div>

    <div class="container-fluid">
      <!-- <base-cards-list cardType="base-card" v-if="showItems" :itemsList="availableServices"></base-cards-list> -->
      <div class="row">
        <div
          class="col-xl-4 mb-5 mb-xl-0"
          v-for="service in availableServices"
          :key="service.id"
          @click="serviceClick(service.id)"
        >
          <div class="card card-profile shadow">
            <div class="row justify-content-center">
              <div class="col-lg-3 order-lg-2">
                <div class="card-profile-image">
                  <img :src="servicePhoto(service.photo)" class="rounded-circle" />
                </div>
              </div>
            </div>
            <div class="card-header text-center border-0 pt-8 pt-md-4 pb-0 pb-md-4"></div>
            <div class="card-body pt-0 pt-md-4 mt-6">
              <div class="text-center">
                <h3>{{ service.companyName }}</h3>
                <div class="h5 font-weight-300">{{ service.address }}</div>
                <hr class="my-4" />
                <p>{{ service.description }}</p>
              </div>
            </div>
          </div>
        </div>
      </div>
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
    //BaseCardsList
  },
  data() {
    return {};
  },
  computed: {
    ...mapState({
      availableServices: state => state.privateItems.availableServices,
      status: state => state.privateItems.status
    }),
    showSpinner() {
      return this.status.availableServicesLoading;
    },
    showItems() {
      return this.status.availableServicesLoaded;
    }
  },
  methods: {
    ...mapActions("privateItems", ["getAvailableServices"]),
    servicePhoto(path) {
      return path
        ? `${process.env.VUE_APP_DEV_BACKEND_URL}/${path}`
        : "img/theme/default_photo.png";
    },
    serviceClick(serviceId) {
      this.$router.push({ name: "service", params: { serviceId: serviceId } });
    }
  },
  mounted() {
    this.getAvailableServices();
  }
};
</script>
<style></style>
