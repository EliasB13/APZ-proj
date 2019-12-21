const state = {
  selectedItems: [],
  isSelectedItemsReseted: false
};

const actions = {
  addSelectedItem({ commit }, id) {
    commit("addSelectedItem", id);
  },
  removeSelectedItem({ commit }, id) {
    commit("removeSelectedItem", id);
  },
  resetSelectedItems({ commit }) {
    commit("resetSelectedItems");
  }
};

const mutations = {
  addSelectedItem(state, id) {
    state.isSelectedItemsReseted = false;
    state.selectedItems.push(id);
  },
  removeSelectedItem(state, id) {
    state.isSelectedItemsReseted = false;
    state.selectedItems = state.selectedItems.filter(idl => idl !== id);
  },
  resetSelectedItems(state) {
    state.selectedItems = [];
    state.isSelectedItemsReseted = true;
  }
};

export const selectedItems = {
  namespaced: true,
  state,
  actions,
  mutations
};
