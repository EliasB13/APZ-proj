import { businessItemsService } from "../../services";
import i18n from "../../localization/i18n";

const state = {
  items: [],
  status: {},
  error: null,
  itemToAdd: null,
  selectedItems: [],
  isSelectedItemsReseted: false
};

const actions = {
  getItems({ commit, dispatch }) {
    commit("getItemsRequest");

    businessItemsService.getItems().then(
      items => commit("getItemsSuccess", items),
      error => {
        dispatch("alert/error", error.toString(), { root: true });
        commit("getItemsFailure", error);
      }
    );
  },
  addItem({ commit, dispatch }, item) {
    commit("addItemRequest", item);

    businessItemsService.addItem(item).then(
      () => {
        commit("addItemSuccess", item);
        dispatch("alert/success", i18n.t("alert.addItemSuccess"), {
          root: true
        });
        dispatch("getItems");
      },
      error => {
        commit("addItemFailure", error);
        dispatch("alert/error", error.toString(), { root: true });
      }
    );
  },
  removeItem({ commit, dispatch }, id) {
    commit("deleteItemRequest", id);

    businessItemsService.delete(id).then(
      () => {
        commit("deleteItemSuccess", id);
        dispatch("alert/success", i18n.t("alert.removeItemSuccess"), {
          root: true
        });
        dispatch("selectedItems/resetSelectedItems", {}, { root: true });
      },
      error => {
        commit("deleteItemFailure", { id, error: error.toString() });
        dispatch("alert/error", error.toString(), { root: true });
      }
    );
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
        const { itemRemoving, ...itemCopy } = item;
        return { ...itemCopy, deleteError: error };
      }

      return item;
    });
  }
};

export const businessItems = {
  namespaced: true,
  state,
  actions,
  mutations
};
