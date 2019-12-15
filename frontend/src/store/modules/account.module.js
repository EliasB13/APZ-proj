import { userService } from "../../services";
import { router } from "../../helpers";

const user = JSON.parse(localStorage.getItem("user"));
const state = user
  ? { status: { loggedIn: true }, user, userToUpdate: {} }
  : { status: {}, user: null, userToUpdate: {} };

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
        dispatch("alert/error", error, { root: true });
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
          // display success message after route change completes
          dispatch("alert/success", "Registration successful", { root: true });
        });
      },
      error => {
        commit("registerFailure", error);
        dispatch("alert/error", error, { root: true });
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
          dispatch("alert/sucess", "Registration sucessful", { root: true });
        });
      },
      error => {
        commit("registerFailure", error);
        dispatch("alert/error", error, { root: true });
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
        dispatch("alert/error", "Account data weren't loaded", { root: true });
      }
    );
  },

  updateUser({ commit, dispatch }, { user, isBusinessUser }) {
    commit("updateUserRequest", user);

    userService.update(user, isBusinessUser).then(
      updatedUser => {
        commit("updateUserSuccess", updatedUser);
        dispatch("alert/success", "User was successfuly updated", {
          root: true
        });
      },
      error => {
        commit("updateUserFailure", error);
        dispatch("alert/error", error, { root: true });
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
