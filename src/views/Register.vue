<template>
  <div class="row justify-content-center">
    <div class="col-lg-5 col-md-7">
      <div class="card bg-secondary shadow border-0">
        <div class="card-header bg-transparent pb-5">
          <div class="text-muted text-center mt-2 mb-3">
            <small>{{ $t("registerPage.signUpAsHeader") }}</small>
          </div>
          <div class="btn-wrapper text-center">
            <base-button
              :type="privateUserSelected ? 'primary' : 'secondary'"
              icon="ni ni-single-02"
              @click="privateUserClick"
              >{{ $t("registerPage.privateUser") }}</base-button
            >
            <base-button
              :type="businessUserSelected ? 'primary' : 'secondary'"
              icon="ni ni-building"
              @click="businessUserClick"
              >{{ $t("registerPage.businessUser") }}</base-button
            >
          </div>
        </div>
        <div v-if="businessUserSelected">
          <div class="card-body px-lg-5 py-lg-5">
            <form role="form">
              <base-input
                class="input-group-alternative mb-3"
                :placeholder="$t('registerPage.placeholder.login')"
                addon-left-icon="ni ni-badge"
                v-model="loginInput"
                :valid="isLoginValid"
                :error="getLoginError"
              ></base-input>

              <base-input
                class="input-group-alternative mb-3"
                :placeholder="$t('registerPage.placeholder.companyName')"
                addon-left-icon="ni ni-badge"
                v-model="companyName"
                :valid="isCompanyNameValid"
                :error="getCompanyNameError"
              ></base-input>

              <base-input
                class="input-group-alternative mb-3"
                :placeholder="$t('registerPage.placeholder.email')"
                type="email"
                addon-left-icon="ni ni-email-83"
                v-model="email"
                :valid="isEmailValid"
              ></base-input>

              <base-input
                class="input-group-alternative mb-3"
                :placeholder="$t('registerPage.placeholder.password')"
                type="password"
                addon-left-icon="ni ni-lock-circle-open"
                v-model="password"
                :valid="isPasswordValid"
                :error="getPasswordError"
              ></base-input>

              <base-input
                class="input-group-alternative"
                :placeholder="
                  $t('registerPage.placeholder.passwordConfirmation')
                "
                type="password"
                addon-left-icon="ni ni-lock-circle-open"
                v-model="passwordConfirmation"
                :valid="isPasswordConfirmationValid"
                :error="getPasswordConfirmationError"
              ></base-input>

              <div class="row my-4">
                <div class="col-12">
                  <base-checkbox class="custom-control-alternative">
                    <span class="text-muted">
                      {{ $t("registerPage.button.policyAgreement") }}
                      <a href="#!">{{
                        $t("registerPage.button.privacyPolicy")
                      }}</a>
                    </span>
                  </base-checkbox>
                </div>
              </div>
              <div class="text-center">
                <base-button
                  type="primary"
                  @click="handleSignUp"
                  class="my-4"
                  >{{ $t("registerPage.button.createAccount") }}</base-button
                >
              </div>
            </form>
          </div>
        </div>
        <div v-if="privateUserSelected">
          <div class="card-body px-lg-5 py-lg-5">
            <form role="form">
              <base-input
                class="input-group-alternative mb-3"
                :placeholder="$t('registerPage.placeholder.login')"
                addon-left-icon="ni ni-badge"
                v-model="loginInput"
                :valid="isLoginValid"
                :error="getLoginError"
              ></base-input>

              <base-input
                class="input-group-alternative mb-3"
                :placeholder="$t('registerPage.placeholder.firstName')"
                addon-left-icon="ni ni-badge"
                v-model="firstName"
                :valid="isFirstNameValid"
                :error="getFirstNameError"
              ></base-input>

              <base-input
                class="input-group-alternative mb-3"
                :placeholder="$t('registerPage.placeholder.lastName')"
                addon-left-icon="ni ni-badge"
                v-model="lastName"
                :valid="isLastNameValid"
                :error="getLastNameError"
              ></base-input>

              <base-input
                class="input-group-alternative mb-3"
                :placeholder="$t('registerPage.placeholder.email')"
                type="email"
                addon-left-icon="ni ni-email-83"
                v-model="email"
                :valid="isEmailValid"
                :error="getEmailError"
              ></base-input>

              <base-input
                class="input-group-alternative mb-3"
                :placeholder="$t('registerPage.placeholder.password')"
                type="password"
                addon-left-icon="ni ni-lock-circle-open"
                v-model="password"
                :valid="isPasswordValid"
                :error="getPasswordError"
              ></base-input>

              <base-input
                class="input-group-alternative"
                :placeholder="
                  $t('registerPage.placeholder.passwordConfirmation')
                "
                type="password"
                addon-left-icon="ni ni-lock-circle-open"
                v-model="passwordConfirmation"
                :valid="isPasswordConfirmationValid"
                :error="getPasswordConfirmationError"
              ></base-input>

              <div class="row my-4">
                <div class="col-12">
                  <base-checkbox class="custom-control-alternative">
                    <span class="text-muted">
                      {{ $t("registerPage.button.policyAgreement") }}
                      <a href="#!">{{
                        $t("registerPage.button.privacyPolicy")
                      }}</a>
                    </span>
                  </base-checkbox>
                </div>
              </div>
              <div class="text-center">
                <b-spinner v-if="showSpinner" small></b-spinner>
                <base-button
                  type="primary"
                  :disabled="
                    !isPasswordValid ||
                      !isEmailValid ||
                      !isPasswordConfirmationValid ||
                      !isLoginValid
                  "
                  @click="handleSignUp"
                  class="my-4"
                >
                  <span>{{ $t("registerPage.button.createAccount") }}</span>
                </base-button>
              </div>
            </form>
          </div>
        </div>
      </div>
      <div class="row mt-3">
        <div class="col-6">
          <a href="#" class="text-light">
            <small>{{ $t("registerPage.button.forgotPassword") }}</small>
          </a>
        </div>
        <div class="col-6 text-right">
          <router-link to="/login" class="text-light">
            <small>{{ $t("registerPage.button.signInRedirect") }}</small>
          </router-link>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { mapState, mapActions } from "vuex";

export default {
  name: "register",
  data() {
    return {
      loginInput: "",
      firstName: "",
      lastName: "",
      email: "",
      password: "",
      companyName: "",
      passwordConfirmation: "",
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
    isFirstNameValid() {
      if (this.firstName === "" && !this.isSubmitted) return null;
      return /^([^0-9,.;\-_+=()"#@!$%\^&*]*)$/.test(this.firstName);
    },
    isLastNameValid() {
      if (this.lastName === "" && !this.isSubmitted) return null;
      return /^([^0-9,.;\-_+=()"#@!$%\^&*]*)$/.test(this.lastName);
    },
    isCompanyNameValid() {
      if (this.companyName === "" && !this.isSubmitted) return null;
      return /^([^,.;+=()"#@!$%\^&*]*)$/.test(this.companyName);
    },
    isEmailValid() {
      if (this.email === "" && !this.isSubmitted) return null;
      var regex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
      return regex.test(String(this.email).toLowerCase());
    },
    isPasswordValid() {
      if (this.password === "" && !this.isSubmitted) return null;
      if (this.password.length < 6 || this.password.length > 23) return false;
      return true;
    },
    isPasswordConfirmationValid() {
      if (this.passwordConfirmation === "" && !this.isSubmitted) return null;
      if (
        this.passwordConfirmation.length < 5 &&
        this.passwordConfirmation.length > 24
      )
        return false;
      if (this.password != this.passwordConfirmation) return false;
      return true;
    },
    getLoginError() {
      if (this.loginInput === "") return "";
      if (this.loginInput.length < 5 || this.loginInput.length > 23)
        return this.$t("registerPage.error.login.length");
      if (this.loginInput.charAt(0) >= "0" && this.loginInput.charAt(0) <= "9")
        return this.$t("registerPage.error.login.startsWithDigit");
      if (!/^[a-zA-z0-9]+$/.test(this.loginInput))
        return this.$t("registerPage.error.login.onlyLatin");
      return "";
    },
    getPasswordError() {
      if (this.password === "") return "";
      if (this.password.length < 6 || this.password.length > 23)
        return this.$t("registerPage.error.password.length");
      return "";
    },
    getPasswordConfirmationError() {
      if (this.password != this.passwordConfirmation)
        return this.$t("registerPage.error.password.passwordConfirmation");
      return "";
    },
    getFirstNameError() {
      if (this.firstName === "") return "";
      if (!/^([^0-9,.;\-_+=()"#@!$%\^&*]*)$/.test(this.firstName))
        return this.$t("registerPage.error.firstName.onlyDigits");
      return "";
    },
    getLastNameError() {
      if (this.lastName === "") return "";
      if (!/^([^0-9,.;\-_+=()"#@!$%\^&*]*)$/.test(this.lastName))
        return this.$t("registerPage.error.lastName.onlyDigits");
      return "";
    },
    getEmailError() {
      if (this.email === "" && !this.isSubmitted) return null;
      var regex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
      if (!regex.test(String(this.email).toLowerCase()))
        return this.$t("registerPage.error.email");
      return "";
    },
    getCompanyNameError() {
      if (this.firstName === "") return "";
      if (!/^([^,.;+=()"#@!$%\^&*]*)$/.test(this.firstName))
        return this.$t("registerPage.error.companyName.onlyDigits");
      return "";
    },
    showSpinner() {
      return this.account.registering;
    }
  },
  methods: {
    ...mapActions("account", ["registerPrivate", "registerBusiness"]),
    ...mapActions("alert", ["clear"]),
    handleSignUp() {
      this.isSubmitted = true;
      const {
        loginInput,
        firstName,
        lastName,
        companyName,
        email,
        password,
        passwordConfirmation
      } = this;
      if (this.privateUserSelected) {
        if (
          loginInput &&
          firstName &&
          lastName &&
          email &&
          password &&
          passwordConfirmation
        ) {
          this.registerPrivate({
            login: loginInput,
            firstName,
            lastName,
            email,
            password,
            passwordConfirmation
          });
        }
      } else {
        if (
          loginInput &&
          companyName &&
          email &&
          password &&
          passwordConfirmation
        ) {
          this.registerBusiness({
            login: loginInput,
            companyName,
            email,
            password,
            passwordConfirmation
          });
        }
      }
    },
    businessUserClick() {
      if (this.privateUserSelected) {
        this.privateUserSelected = false;
        this.businessUserSelected = true;
        this.resetInputs();
      }
    },
    privateUserClick() {
      if (this.businessUserSelected) {
        this.businessUserSelected = false;
        this.privateUserSelected = true;
        this.resetInputs();
      }
    },
    resetInputs() {
      (this.loginInput = ""),
        (this.firstName = ""),
        (this.lastName = ""),
        (this.email = ""),
        (this.password = ""),
        (this.companyName = ""),
        (this.passwordConfirmation = ""),
        (this.isSubmitted = false);
    }
  }
};
</script>
<style></style>
