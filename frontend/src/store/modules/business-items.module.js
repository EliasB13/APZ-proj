import { businessItemsService } from "../../services";

const state = {
  items: [],
  status: {},
  error: null,
  itemToAdd: null,
  selectedItems: [],
  isSelectedItemsReseted: false
};

const actions = {
  getItems({ commit }) {
    commit("getItemsRequest");

    businessItemsService.getItems().then(
      items => commit("getItemsSuccess", items),
      error => commit("getItemsFailure", error)
    );
  },
  addItem({ commit, dispatch }, item) {
    commit("addItemRequest", item);

    businessItemsService.addItem(item).then(
      () => {
        commit("addItemSuccess", item);
        dispatch("alert/success", "Item was added", { root: true });
      },
      error => {
        commit("addItemFailure", error);
        dispatch("alert/error", error, { root: true });
      }
    );
  },
  removeItem({ commit }, id) {
    commit("deleteItemRequest", id);

    businessItemsService.delete(id).then(
      () => {
        commit("deleteItemSuccess", id);
        commit("removeSelectedItem", id);
      },
      error => commit("deleteItemFailure", { id, error: error.toString() })
    );
  },

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
  getItemsRequest(state) {
    state.status = { itemsLoading: true };
  },
  getItemsSuccess(state, items) {
    state.status = { itemsLoaded: true };
    state.items = items;
  },
  getItemsFailure(state, error) {
    state.status = {};
    state.items = [];
    state.error = error;
  },

  addItemRequest(state, item) {
    state.itemToAdd = item;
    state.status = { ...state.status, itemAdding: true };
  },
  addItemSuccess(state, item) {
    state.status = { ...state.status, itemAdded: true, itemAdding: false };
    state.items.push(item);
  },
  addItemFailure(state, error) {
    state.status = { ...state.status, itemAdded: false, itemAdding: false };
    state.error = error;
  },

  deleteItemRequest(state, id) {
    state.status = { ...state.status, itemRemoving: true };
    state.items = state.items.map(item =>
      item.id === id ? { ...item, itemRemoving: true } : item
    );
  },
  deleteItemSuccess(state, id) {
    state.status = { ...state.status, itemRemoved: true, itemRemoving: false };
    state.items = state.items.filter(item => item.id !== id);
  },
  deleteItemFailure(state, { id, error }) {
    state.status = { ...state.status, itemRemoved: false, itemRemoving: false };
    state.items = state.items.map(item => {
      if (item.id === id) {
        const { deleting, ...userCopy } = item;
        return { ...userCopy, deleteError: error };
      }

      return item;
    });
  },

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

export const businessItems = {
  namespaced: true,
  state,
  actions,
  mutations
};
