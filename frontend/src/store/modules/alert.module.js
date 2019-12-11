const state = {
  type: null,
  message: null,
  stateChanger: 0
};

const actions = {
  success({ commit }, message) {
    commit("success", message);
  },
  error({ commit }, message) {
    commit("error", message);
  },
  clear({ commit }, message) {
    commit("success", message);
  }
};

const mutations = {
  success(state, message) {
    state.type = "success";
    state.message = message;
    ++state.stateChanger;
  },
  error(state, message) {
    state.type = "danger";
    state.message = message;
    ++state.stateChanger;
  },
  clear(state) {
    state.type = null;
    state.message = null;
    state.stateChanger = 0;
  }
};

export const alert = {
  namespaced: true,
  state,
  actions,
  mutations
};
