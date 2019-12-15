<template>
  <div>
    <base-header type="gradient-success" class="header pb-8 pt-5 pt-lg-8 d-flex align-items-center">
      <div class="container-fluid d-flex align-items-center">
        <div class="row">
          <div class="col-lg-7 col-md-10">
            <h1 class="display-2 text-white mb-5">{{ service.companyName }}</h1>
          </div>
        </div>
      </div>
    </base-header>

    <div class="container-fluid mt--7">
      <div class="row">
        <div class="col-xl-4 order-xl-2 mb-5 mb-xl-0">
          <div class="card card-profile shadow">
            <div class="row justify-content-center">
              <div class="col-lg-3 order-lg-2">
                <div class="card-profile-image">
                  <img :src="userPhoto" class="rounded-circle" />
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

        <div class="col-xl-8 order-xl-1">
          <base-cards-list
            colsClasses="col-lg-4 col-xl-4"
            cardType="base-card"
            v-if="showItems"
            :itemsList="serviceItems"
          ></base-cards-list>
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
import { mapActions, mapState } from "vuex";
import BaseCardsList from "../../components/BaseCardsList";

export default {
  components: {
    BaseCardsList
  },
  props: {
    serviceId: [String, Number]
  },
  computed: {
    ...mapState({
      service: state => state.account.publicProfile,
      profileStatus: state => state.account.status,
      serviceItems: state => state.privateItems.businessItems,
      itemsStatus: state => state.privateItems.status
    }),
    userPhoto() {
      return this.service.photo
        ? `${process.env.VUE_APP_DEV_BACKEND_URL}/${this.service.photo}`
        : "/img/theme/default_photo.png";
    },
    showSpinner() {
      return this.profileStatus.publicProfileLoading;
    },
    showItems() {
      return this.itemsStatus.businessItemsLoaded;
    }
  },
  methods: {
    ...mapActions("account", ["getPublicProfile"]),
    ...mapActions("privateItems", ["getBusinessItemsByBusinessUser"])
  },
  mounted() {
    this.getPublicProfile({
      isBusinessUser: true,
      userId: parseInt(this.serviceId)
    });
    this.getBusinessItemsByBusinessUser(this.serviceId);
  }
};
</script>