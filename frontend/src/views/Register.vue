<template>
  <div class="row justify-content-center">
    <div class="col-lg-5 col-md-7">
      <div class="card bg-secondary shadow border-0">
        <div class="card-header bg-transparent pb-5">
          <div class="text-muted text-center mt-2 mb-3">
            <small>Sign up as</small>
          </div>
          <div class="btn-wrapper text-center">
            <base-button
              :type="privateUserSelected ? 'primary' : 'secondary'"
              icon="ni ni-single-02"
              @click="privateUserClick"
            >Private user</base-button>
            <base-button
              :type="businessUserSelected ? 'primary' : 'secondary'"
              icon="ni ni-building"
              @click="businessUserClick"
            >Business user</base-button>
          </div>
        </div>
        <div v-if="businessUserSelected">
          <div class="card-body px-lg-5 py-lg-5">
            <form role="form">
              <base-input
                class="input-group-alternative mb-3"
                placeholder="Login"
                addon-left-icon="ni ni-badge"
                v-model="loginInput"
                :valid="isLoginValid"
                :error="getLoginError"
              ></base-input>

              <base-input
                class="input-group-alternative mb-3"
                placeholder="Company name"
                addon-left-icon="ni ni-badge"
                v-model="companyName"
                :valid="isCompanyNameValid"
                :error="getCompanyNameError"
              ></base-input>

              <base-input
                class="input-group-alternative mb-3"
                placeholder="Email"
                type="email"
                addon-left-icon="ni ni-email-83"
                v-model="email"
                :valid="isEmailValid"
              ></base-input>

              <base-input
                class="input-group-alternative mb-3"
                placeholder="Password"
                type="password"
                addon-left-icon="ni ni-lock-circle-open"
                v-model="password"
                :valid="isPasswordValid"
                :error="getPasswordError"
              ></base-input>

              <base-input
                class="input-group-alternative"
                placeholder="Confirm password"
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
                      I agree with the
                      <a href="#!">Privacy Policy</a>
                    </span>
                  </base-checkbox>
                </div>
              </div>
              <div class="text-center">
                <base-button type="primary" @click="handleSignUp" class="my-4">Create account</base-button>
              </div>
            </form>
          </div>
        </div>
        <div v-if="privateUserSelected">
          <div class="card-body px-lg-5 py-lg-5">
            <form role="form">
              <base-input
                class="input-group-alternative mb-3"
                placeholder="Login"
                addon-left-icon="ni ni-badge"
                v-model="loginInput"
                :valid="isLoginValid"
                :error="getLoginError"
              ></base-input>

              <base-input
                class="input-group-alternative mb-3"
                placeholder="First name"
                addon-left-icon="ni ni-badge"
                v-model="firstName"
                :valid="isFirstNameValid"
                :error="getFirstNameError"
              ></base-input>

              <base-input
                class="input-group-alternative mb-3"
                placeholder="Last name"
                addon-left-icon="ni ni-badge"
                v-model="lastName"
                :valid="isLastNameValid"
                :error="getLastNameError"
              ></base-input>

              <base-input
                class="input-group-alternative mb-3"
                placeholder="Email"
                type="email"
                addon-left-icon="ni ni-email-83"
                v-model="email"
                :valid="isEmailValid"
                :error="getEmailError"
              ></base-input>

              <base-input
                class="input-group-alternative mb-3"
                placeholder="Password"
                type="password"
                addon-left-icon="ni ni-lock-circle-open"
                v-model="password"
                :valid="isPasswordValid"
                :error="getPasswordError"
              ></base-input>

              <base-input
                class="input-group-alternative"
                placeholder="Confirm password"
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
                      I agree with the
                      <a href="#!">Privacy Policy</a>
                    </span>
                  </base-checkbox>
                </div>
              </div>
              <div class="text-center">
                <base-button type="primary" @click="handleSignUp" class="my-4">Create account</base-button>
              </div>
              <div class="alert-div">
                <b-alert
                  :show="showAlert"
                  dismissible
                  @dismissed="clearStoreAlerts"
                >{{ alert.message }}</b-alert>
              </div>
            </form>
          </div>
        </div>
      </div>
      <div class="row mt-3">
        <div class="col-6">
          <a href="#" class="text-light">
            <small>Forgot password?</small>
          </a>
        </div>
        <div class="col-6 text-right">
          <router-link to="/login" class="text-light">
            <small>Login into your account</small>
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
      if (this.password.length < 5 && this.password.length > 24) return false;
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
        return "Login must be more than 4 letters and less than 24";
      if (this.loginInput.charAt(0) >= "0" && this.loginInput.charAt(0) <= "9")
        return "Login can't starts with digit";
      if (!/^[a-zA-z0-9]+$/.test(this.loginInput))
        return "Login can contain only latin chars";
      return "";
    },
    getPasswordError() {
      if (this.password === "") return "";
      if (this.password.length < 6 || this.password.length > 23)
        return "Password must be more than 6 symbols and less than 24";
      return "";
    },
    getPasswordConfirmationError() {
      if (this.password != this.passwordConfirmation)
        return "Password and confirmation don't match";
      return "";
    },
    getFirstNameError() {
      if (this.firstName === "") return "";
      if (!/^([^0-9,.;\-_+=()"#@!$%\^&*]*)$/.test(this.firstName))
        return "First name can contain only letters";
      return "";
    },
    getLastNameError() {
      if (this.lastName === "") return "";
      if (!/^([^0-9,.;\-_+=()"#@!$%\^&*]*)$/.test(this.lastName))
        return "Last name can contain only letters";
      return "";
    },
    getEmailError() {
      if (this.email === "" && !this.isSubmitted) return null;
      var regex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
      if (!regex.test(String(this.email).toLowerCase()))
        return "Provide valid email, e. g. user@example.com";
      return "";
    },
    showAlert() {
      return this.alert && this.alert.message;
    },
    getCompanyNameError() {
      if (this.firstName === "") return "";
      if (!/^([^,.;+=()"#@!$%\^&*]*)$/.test(this.firstName))
        return "First name can contain only letters";
      return "";
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
    clearStoreAlerts() {
      this.clear();
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
  },
  watch: {
    $route(to, from) {
      this.clearStoreAlerts();
    }
  }
};
</script>
<style></style>
