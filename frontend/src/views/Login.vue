<template>
  <div class="row justify-content-center">
    <div class="col-lg-5 col-md-7">
      <div class="card bg-secondary shadow border-0">
        <div class="card-header bg-transparent pb-5">
          <div class="text-muted text-center mt-2 mb-3">
            <small>{{ $t("loginPage.signInAsHeader") }}</small>
          </div>
          <div class="btn-wrapper text-center">
            <base-button
              :type="privateUserSelected ? 'primary' : 'secondary'"
              icon="ni ni-single-02"
              @click="privateUserClick"
              >{{ $t("loginPage.privateUser") }}</base-button
            >
            <base-button
              :type="businessUserSelected ? 'primary' : 'secondary'"
              icon="ni ni-building"
              @click="businessUserClick"
              >{{ $t("loginPage.businessUser") }}</base-button
            >
          </div>
        </div>
        <div class="card-body px-lg-5 py-lg-5">
          <form role="form">
            <base-input
              class="input-group-alternative mb-3"
              :placeholder="$t('loginPage.placeholder.login')"
              addon-left-icon="ni ni-email-83"
              v-model="loginInput"
            ></base-input>

            <base-input
              class="input-group-alternative"
              :placeholder="$t('loginPage.placeholder.password')"
              type="password"
              addon-left-icon="ni ni-lock-circle-open"
              v-model="passwordInput"
            ></base-input>

            <base-checkbox class="custom-control-alternative">
              <span class="text-muted">{{
                $t("loginPage.button.rememberMe")
              }}</span>
            </base-checkbox>
            <div class="text-center">
              <b-spinner v-if="showSpinner" class="mr-3"></b-spinner>
              <base-button @click="handleSignIn" type="primary" class="my-4">{{
                $t("loginPage.button.signIn")
              }}</base-button>
            </div>
          </form>
        </div>
      </div>
      <div class="row mt-3">
        <div class="col-6">
          <a href="#" class="text-light">
            <small>{{ $t("loginPage.button.forgotPassword") }}</small>
          </a>
        </div>
        <div class="col-6 text-right">
          <router-link to="/register" class="text-light">
            <small>{{ $t("loginPage.button.signUpRedirect") }}</small>
          </router-link>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState, mapActions } from "vuex";

export default {
  name: "loginView",
  data() {
    return {
      loginInput: "",
      passwordInput: "",
      privateUserSelected: true,
      businessUserSelected: false,
      isSubmitted: false
    };
  },
  computed: {
    ...mapState({
      account: state => state.account.status,
      alert: state => state.alert
    }),
    isLoginValid() {
      if (this.loginInput === "" && !this.isSubmitted) return null;
      return this.loginInput.length > 4 && this.loginInput.length < 24;
    },
    getLoginError() {
      if (this.loginInput === "") return "";
      if (this.loginInput.length < 5 || this.loginInput.length > 23)
        return "Login must be more than 4 letters and less than 24";
      if (this.loginInput.charAt(0) >= "0" && this.loginInput.charAt(0) <= "9")
        return "Login can't starts with digit";
      if (!/^[a-zA-z0-9]+$/.test(this.loginInput))
        return "Login can contain only latin chars";
      return "";
    },
    isPasswordValid() {
      if (this.passwordInput === "" && !this.isSubmitted) return null;
      if (this.passwordInput.length < 6 || this.passwordInput.length > 23)
        return false;
      return true;
    },
    getPasswordError() {
      if (this.passwordInput === "" && !this.isSubmitted) return "";
      if (this.passwordInput.length < 6 || this.passwordInput.length > 23)
        return "Password must be more than 5 symbols and less than 24";
      return "";
    },
    showSpinner() {
      return this.account.loggingIn;
    }
  },
  created() {
    this.logout();
  },
  methods: {
    ...mapActions("account", ["login", "logout"]),
    ...mapActions("alert", ["clear"]),
    handleSignIn() {
      this.isSubmitted = true;
      const { loginInput, passwordInput } = this;
      if (loginInput && passwordInput) {
        this.login({
          login: loginInput,
          password: passwordInput,
          isBusinessUser: this.businessUserSelected
        });
      }
    },
    clearStoreAlerts() {
      this.clear();
    },
    businessUserClick() {
      if (this.privateUserSelected) {
        this.privateUserSelected = false;
        this.businessUserSelected = true;
      }
    },
    privateUserClick() {
      if (this.businessUserSelected) {
        this.businessUserSelected = false;
        this.privateUserSelected = true;
      }
    }
  }
};
</script>
<style></style>
