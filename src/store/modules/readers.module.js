import i18n from "../../localization/i18n";
import { readersService } from "../../services";

const state = {
  readers: [],
  items: [],
  status: {},
  error: null,
  itemToAdd: null
};

const actions = {
  getReaders({ commit, dispatch }) {
    commit("getReadersRequest");

    readersService.getReaders().then(
      readers => commit("getReadersSuccess", readers),
      error => {
        commit("getReadersFailure", error);
        dispatch("alert/error", error.toString(), { root: true });
      }
    );
  },
  orderReader({ commit, dispatch }) {
    commit("orderReaderRequest");

    readersService.orderReader().then(
      reader => {
        commit("orderReaderSuccess", reader);
        dispatch("alert/success", i18n.t("alert.orderReaderSuccess"), {
          root: true
        });
      },
      error => {
        commit("orderReaderFailure", error);
        dispatch("alert/error", error.toString(), { root: true });
      }
    );
  },
  addItemToReader({ commit, dispatch }, { itemId, readerId }) {
    commit("addItemToReaderRequest");

    readersService.addItemToReader(itemId, readerId).then(
      reader => {
        commit("addItemToReaderSuccess", reader);
        dispatch("alert/success", i18n.t("alert.addItemToReaderSuccess"), {
          root: true
        });
      },
      error => {
        commit("addItemToReaderFailure", error);
        dispatch("alert/error", error.toString(), { root: true });
      }
    );
  },
  removeItemFromReader({ commit, dispatch }, itemId) {
    commit("removeItemFromReaderRequest");

    readersService.removeItemFromReader(itemId).then(
      reader => {
        commit("removeItemFromReaderSuccess", itemId);
        dispatch("alert/success", i18n.t("alert.removeItemFromReaderSuccess"), {
          root: true
        });
      },
      error => {
        commit("removeItemFromReaderFailure", error);
        dispatch("alert/error", error.toString(), { root: true });
      }
    );
  },
  getReaderItems({ commit, dispatch }, readerId) {
    commit("getReaderItemsRequest");

    readersService.getReaderItems(readerId).then(
      items => commit("getReaderItemsSuccess", items),
      error => {
        commit("getReaderItemsFailure", error);
        dispatch("alert/error", error.toString(), { root: true });
      }
    );
  },
  resetState({ commit }) {
    commit("resetState");
  }
};

const mutations = {
  getReadersRequest(state) {
    state.status = { readersLoading: true };
  },
  getReadersSuccess(state, readers) {
    state.status = { readersLoading: false, readersLoaded: true };
    state.readers = readers;
  },
  getReadersFailure(state, error) {
    state.status = {};
    state.readers = [];
    state.error = error;
  },

  orderReaderRequest(state) {
    state.status = { ...state.status, readerOrdering: true };
  },
  orderReaderSuccess(state, reader) {
    state.status = {
      ...state.status,
      readerOrdering: false,
      readerOrdered: true
    };
    state.readers.push(reader);
  },
  orderReaderFailure(state, error) {
    state.status = {
      ...state.status,
      readerOrdering: false,
      readerOrdered: false
    };
    state.error = error;
  },

  addItemToReaderRequest(state, item) {
    state.status = {
      ...state.status,
      itemAdding: true
    };
    state.itemToAdd = item;
  },
  addItemToReaderSuccess(state, item) {
    state.status = {
      ...state.status,
      itemAdding: false,
      itemAdded: true
    };
    state.items.push(item);
  },
  addItemToReaderFailure(state, error) {
    state.status = {
      ...state.status,
      itemAdding: false,
      itemAdded: false
    };
    state.error = error;
    state.itemToAdd = {};
  },

  removeItemFromReaderRequest(state) {
    state.status = {
      ...state.status,
      itemRemoving: true
    };
  },
  removeItemFromReaderSuccess(state, itemId) {
    state.status = {
      ...state.status,
      itemRemoving: false,
      itemRemoved: true
    };
    debugger;
    state.items = state.items.filter(i => i.id !== itemId);
  },
  removeItemFromReaderFailure(state, error) {
    state.status = {
      ...state.status,
      itemRemoving: false,
      itemRemoved: false
    };
    state.error = error;
  },

  getReaderItemsRequest(state) {
    state.status = { ...state.status, readerItemsLoading: true };
  },
  getReaderItemsSuccess(state, items) {
    state.status = {
      ...state.status,
      readerItemsLoading: false,
      readerItemsLoaded: true
    };
    state.items = items;
  },
  getReaderItemsFailure(state, error) {
    state.status = {
      ...state.status,
      readerItemsLoading: false,
      readerItemsLoaded: false
    };
    state.items = [];
    state.error = error;
  },
  resetState(state) {
    state.status = {};
    state.error = {};
    state.items = [];
  }
};

export const readers = {
  namespaced: true,
  state,
  actions,
  mutations
};
