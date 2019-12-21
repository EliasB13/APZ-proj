import { userService } from "../../services";
import { router } from "../../helpers";
import i18n from "../../localization/i18n";

const user = JSON.parse(localStorage.getItem("user"));
const state = user
  ? { status: { loggedIn: true }, user, userToUpdate: {}, publicProfile: {} }
  : { status: {}, user: null, userToUpdate: {}, publicProfile: {} };

const actions = {
  login({ dispatch, commit }, { login, password, isBusinessUser }) {
    commit("loginRequest", { login, isBusinessUser });
    userService.login(login, password, isBusinessUser).then(
      user => {
        commit("loginSuccess", user);
        if (isBusinessUser) router.push("/business-items");
        else router.push("/availableServices");
      },
      error => {
        commit("loginFailure", error);
        dispatch("alert/error", error.toString(), { root: true });
      }
    );
  },
  logout({ commit }) {
    userService.logout();
    commit("logout");
  },
  registerPrivate({ dispatch, commit }, user) {
    commit("registerRequest", user);

    userService.register(user, false).then(
      user => {
        commit("registerSuccess", user);
        router.push("/login");
        setTimeout(() => {
          dispatch("alert/success", i18n.t("alert.registerSuccess"), {
            root: true
          });
        });
      },
      error => {
        commit("registerFailure", error);
        dispatch("alert/error", error.toString(), { root: true });
      }
    );
  },
  registerBusiness({ dispatch, commit }, user) {
    commit("registerRequest", user);

    userService.register(user, true).then(
      user => {
        commit("registerSuccess", user);
        router.push("/login");
        setTimeout(() => {
          dispatch("alert/sucess", i18n.t("alert.registerSuccess"), {
            root: true
          });
        });
      },
      error => {
        commit("registerFailure", error);
        dispatch("alert/error", error.toString(), { root: true });
      }
    );
  },
  getAccountData({ commit, dispatch }, isBusinessUser) {
    commit("getAccountDataRequest", isBusinessUser);

    userService.getAccountData(isBusinessUser).then(
      accountData => {
        commit("getAccountDataSuccess", accountData);
      },
      error => {
        commit("getAccountDataFailure", error);
        dispatch("alert/error", error.toString(), { root: true });
      }
    );
  },

  getPublicProfile({ commit, dispatch }, { isBusinessUser, userId }) {
    commit("getPublicProfileRequest");

    userService.getPublicProfile(isBusinessUser, userId).then(
      profile => commit("getPublicProfileSuccess", profile),
      error => {
        commit("getPublicProfileFailure", error);
        dispatch("alert/error", error.toString(), { root: true });
      }
    );
  },

  updateUser({ commit, dispatch }, { user, isBusinessUser }) {
    commit("updateUserRequest", user);

    userService.update(user, isBusinessUser).then(
      updatedUser => {
        commit("updateUserSuccess", updatedUser);
        dispatch("alert/success", i18n.t("alert.updateUserSuccess"), {
          root: true
        });
      },
      error => {
        console.log(error);
        commit("updateUserFailure", error);
        dispatch("alert/error", error.toString(), { root: true });
      }
    );
  }
};

const mutations = {
  loginRequest(state, user) {
    state.status = { loggingIn: true };
    state.user = user;
  },
  loginSuccess(state, user) {
    state.status = { loggedIn: true };
    state.user = user;
  },
  loginFailure(state, error) {
    state.status = {};
    state.user = null;
    state.error = error;
  },

  logout(state) {
    state.status = {};
    state.user = null;
  },

  registerRequest(state, user) {
    state.status = { registering: true };
  },
  registerSuccess(state, user) {
    state.status = {};
  },
  registerFailure(state, error) {
    state.status = {};
    state.error = error;
  },

  getAccountDataRequest(state, isBusinessUser) {
    state.status = { ...state.status, accountDataLoading: true };
  },
  getAccountDataSuccess(state, accountData) {
    state.status = {
      ...state.status,
      accountDataLoading: false,
      accountDataLoaded: false
    };
    state.user = { ...state.user, ...accountData };
  },
  getAccountDataFailure(state, error) {
    state.status = {};
    state.error = error;
  },

  getPublicProfileRequest(state) {
    state.status = { ...state.status, publicProfileLoading: true };
  },
  getPublicProfileSuccess(state, profile) {
    (state.status = {
      ...state.status,
      publicProfileLoading: false,
      publicProfileLoaded: true
    }),
      (state.publicProfile = profile);
  },
  getPublicProfileFailure(state, error) {
    (state.status = {
      ...state.status,
      publicProfileLoading: false,
      publicProfileLoaded: false
    }),
      (state.publicProfile = {});
    state.error = error;
  },

  updateUserRequest(state, user) {
    state.status = { ...state.status, userUpdating: true };
    state.userToUpdate = user;
  },
  updateUserSuccess(state, updatedUser) {
    state.status = { ...state.status, userUpdating: false, userUpdated: true };
    state.user = { ...state.user, ...updatedUser };
  },
  updateUserFailure(state, error) {
    state.status = { ...state.status, userUpdating: false, userUpdated: false };
    state.userToUpdate = {};
    state.error = error;
  }
};

export const account = {
  namespaced: true,
  state,
  actions,
  mutations
};
