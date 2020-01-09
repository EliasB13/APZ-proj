<template>
  <div>
    <base-header type="gradient-primary" class="header pb-8 pt-5 pt-lg-8 d-flex align-items-center">
      <div class="container-fluid d-flex align-items-center">
        <div class="row">
          <div class="col-lg-7 col-md-10">
            <h1
              class="display-2 text-white"
            >{{ $t("businessProfilePage.mainHeader", { name: user.login }) }}</h1>
            <p class="text-white mt-0 mb-5">{{ $t("businessProfilePage.secondaryHeader") }}</p>
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
                  <a href="#">
                    <img :src="userPhoto" class="rounded-circle" />
                  </a>
                </div>
              </div>
            </div>
            <div class="card-header text-center border-0 pt-8 pt-md-4 pb-0 pb-md-4"></div>
            <div class="card-body pt-0 pt-md-4 mt-6">
              <div class="text-center">
                <h3>{{ user.companyName }}</h3>
                <div class="h5 font-weight-300">{{ user.address }}</div>
                <hr class="my-4" />
                <p>{{ user.description }}</p>
              </div>
            </div>
          </div>
        </div>

        <div v-if="!editingMode" class="col-xl-8 order-xl-1">
          <card shadow type="secondary">
            <div slot="header" class="bg-white border-0">
              <div class="row align-items-center">
                <div class="col-8">
                  <h3 class="mb-0">{{ $t("businessProfilePage.profileCardHeader") }}</h3>
                </div>
                <div class="col-4 text-right">
                  <div @click="editClick" class="btn btn-sm btn-primary">Edit</div>
                </div>
              </div>
            </div>
            <template>
              <form @submit.prevent>
                <h6
                  class="heading-small text-muted mb-4"
                >{{ $t("businessProfilePage.userInformationHeader") }}</h6>
                <div class="pl-lg-4">
                  <div class="row">
                    <div class="col-lg-6">
                      <label class="form-control-label">
                        {{
                        $t("businessProfilePage.label.login")
                        }}
                      </label>
                      <p>{{ user.login }}</p>
                    </div>
                    <div class="col-lg-6">
                      <label class="form-control-label">
                        {{
                        $t("businessProfilePage.label.email")
                        }}
                      </label>
                      <p>{{ user.email }}</p>
                    </div>
                  </div>
                </div>
                <hr class="my-4" />
                <h6
                  class="heading-small text-muted mb-4"
                >{{ $t("businessProfilePage.contactInformationHeader") }}</h6>
                <div class="pl-lg-4">
                  <div class="row">
                    <div class="col-md-12">
                      <label class="form-control-label">
                        {{
                        $t("businessProfilePage.label.companyName")
                        }}
                      </label>
                      <p>{{ user.companyName }}</p>
                    </div>
                  </div>
                  <div class="row">
                    <div class="col-lg-6">
                      <label class="form-control-label">
                        {{
                        $t("businessProfilePage.label.address")
                        }}
                      </label>
                      <p>{{ user.address ? user.address : "-" }}</p>
                    </div>
                    <div class="col-lg-6">
                      <label class="form-control-label">
                        {{
                        $t("businessProfilePage.label.phone")
                        }}
                      </label>
                      <p>{{ user.phone ? user.phone : "-" }}</p>
                    </div>
                  </div>
                </div>
                <hr class="my-4" />
                <h6
                  class="heading-small text-muted mb-4"
                >{{ $t("businessProfilePage.descriptionHeader") }}</h6>
                <div class="pl-lg-4">
                  <div class="form-group">
                    <base-input alternative :label="$t('businessProfilePage.label.description')">
                      <p>{{ user.description ? user.description : "-" }}</p>
                    </base-input>
                  </div>
                </div>
              </form>
            </template>
          </card>
        </div>

        <div v-else-if="editingMode" class="col-xl-8 order-xl-1">
          <card shadow type="secondary">
            <div slot="header" class="bg-white border-0">
              <div class="row align-items-center">
                <div class="col-8">
                  <h3 class="mb-0">{{ $t("businessProfilePage.profileCardHeader") }}</h3>
                </div>
                <div class="col-4 text-right">
                  <div @click="saveClick" class="btn btn-sm btn-success">{{ $t("common.saveBtn") }}</div>
                </div>
              </div>
            </div>
            <template>
              <form @submit.prevent>
                <h6
                  class="heading-small text-muted mb-4"
                >{{ $t("businessProfilePage.userInformationHeader") }}</h6>
                <div class="pl-lg-4">
                  <div class="row">
                    <div class="col-lg-6">
                      <base-input
                        alternative
                        :label="$t('businessProfilePage.label.login')"
                        :placeholder="
                          $t('businessProfilePage.placeholder.login')
                        "
                        input-classes="form-control-alternative"
                        v-model="model.login"
                      />
                    </div>
                    <div class="col-lg-6">
                      <base-input
                        alternative
                        :label="$t('businessProfilePage.label.email')"
                        placeholder="jesse@example.com"
                        input-classes="form-control-alternative"
                        v-model="model.email"
                      />
                    </div>
                  </div>
                </div>
                <hr class="my-4" />
                <h6
                  class="heading-small text-muted mb-4"
                >{{ $t("businessProfilePage.contactInformationHeader") }}</h6>
                <div class="pl-lg-4">
                  <div class="row">
                    <div class="col-md-12">
                      <base-input
                        alternative
                        :label="$t('businessProfilePage.label.companyName')"
                        :placeholder="
                          $t('businessProfilePage.placeholder.companyName')
                        "
                        input-classes="form-control-alternative"
                        v-model="model.companyName"
                      />
                    </div>
                  </div>
                  <div class="row">
                    <div class="col-lg-6">
                      <base-input
                        alternative
                        :label="$t('businessProfilePage.label.address')"
                        :placeholder="
                          $t('businessProfilePage.placeholder.address')
                        "
                        input-classes="form-control-alternative"
                        v-model="model.address"
                      />
                    </div>
                    <div class="col-lg-6">
                      <base-input
                        alternative
                        :label="$t('businessProfilePage.label.phone')"
                        :placeholder="
                          $t('businessProfilePage.placeholder.phone')
                        "
                        input-classes="form-control-alternative"
                        v-model="model.phone"
                      />
                    </div>
                  </div>
                </div>
                <hr class="my-4" />
                <h6
                  class="heading-small text-muted mb-4"
                >{{ $t("businessProfilePage.descriptionHeader") }}</h6>
                <div class="pl-lg-4">
                  <div class="form-group">
                    <base-input alternative :label="$t('businessProfilePage.label.description')">
                      <textarea
                        rows="4"
                        class="form-control form-control-alternative"
                        :placeholder="
                          $t('businessProfilePage.placeholder.description')
                        "
                        v-model="model.description"
                      ></textarea>
                    </base-input>
                  </div>
                </div>
              </form>
            </template>
          </card>
        </div>
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
import { mapActions, mapState } from "vuex";

export default {
  name: "user-profile",
  computed: {
    ...mapState({
      user: state => state.account.user,
      status: state => state.account.status
    }),
    userPhoto() {
      if (this.user.photo)
        return `${process.env.VUE_APP_DEV_BACKEND_URL}/${this.user.photo}`;
      return `/img/theme/default_photo.png`;
    },
    showSpinner() {
      return this.status.accountDataLoading || this.status.userUpdating;
    }
  },
  data() {
    return {
      model: {
        login: "",
        email: "",
        address: "",
        description: "",
        phone: "",
        companyName: "",
        photo: ""
      },
      editingMode: false
    };
  },
  methods: {
    ...mapActions("account", ["getAccountData", "updateUser"]),
    editClick() {
      this.model.login = this.user.login;
      this.model.email = this.user.email;
      this.model.address = this.user.address;
      this.model.description = this.user.description;
      this.model.phone = this.user.phone;
      this.model.companyName = this.user.companyName;
      this.model.photo = this.user.photo;
      this.editingMode = true;
    },
    saveClick() {
      this.updateUser({
        user: this.model,
        isBusinessUser: this.user.isBusinessUser
      });
      this.editingMode = false;
    }
  },
  mounted() {
    this.getAccountData(this.user.isBusinessUser);
  }
};
</script>
<style></style>
