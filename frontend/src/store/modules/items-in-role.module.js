import { rolesService } from "../../services";

const state = {
  items: [],
  status: {},
  itemToAdd: {},
  error: null
};

const actions = {
  getItemsInRole({ commit, dispatch }, roleId) {
    commit("getItemsByRoleRequest", roleId);

    rolesService.getItemsInRole(roleId).then(
      items => {
        commit("getItemsByRoleSuccess", items);
      },
      error => {
        commit("getItemsByRole", error);
        dispatch("alert/error", error.toString(), {
          root: true
        });
      }
    );
  },
  addItemToRole({ commit, dispatch }, { itemId, roleId }) {
    commit("addItemToRoleRequest", { itemId, roleId });

    rolesService.addItemToRole(itemId, roleId).then(
      item => {
        commit("addItemToRoleSuccess", item);
        dispatch("alert/success", "item was added to role", { root: true });
      },
      error => {
        commit("addItemToRoleFailure", error);
        dispatch("alert/error", error.toString(), {
          root: true
        });
      }
    );
  },
  removeItemFromRole({ commit, dispatch }, roleItemId) {
    commit("removeItemFromRoleRequest", roleItemId);

    rolesService.removeItemFromRole(roleItemId).then(
      () => {
        commit("removeItemFromRoleSuccess", roleItemId);
        dispatch("alert/success", "item was removed from role", {
          root: true
        });
        dispatch("selectedItems/resetSelectedItems", {}, { root: true });
      },
      error => {
        commit("removeItemFromRoleFailure", error);
        dispatch("alert/error", error.toString(), { root: true });
      }
    );
  },
  resetState({ commit }) {
    commit("resetState");
  }
};

const mutations = {
  getItemsByRoleRequest(state, roleId) {
    state.status = { ...state.status, itemsLoading: true };
  },
  getItemsByRoleSuccess(state, items) {
    state.status = {
      ...state.status,
      itemsLoading: false,
      itemsLoaded: true
    };
    state.items = items;
  },
  getItemsByRoleFailure(state, error) {
    state.status = {
      ...state.status,
      itemsLoading: false,
      itemsLoaded: false
    };
    state.items = [];
    state.error = error;
  },

  addItemToRoleRequest(state, empl) {
    state.status = {
      ...state.status,
      itemAdding: true
    };
    state.itemToAdd = empl;
  },
  addItemToRoleSuccess(state, empl) {
    state.status = {
      ...state.status,
      itemAdding: false,
      itemAdded: false
    };
    state.items.push(empl);
  },
  addItemToRoleFailure(state, error) {
    state.status = {
      ...state.status,
      itemAdding: false,
      itemAdded: false
    };
    state.error = error;
  },

  removeItemFromRoleRequest(state, roleItemId) {
    state.status = {
      ...state.status,
      itemRemoving: true
    };
  },
  removeItemFromRoleSuccess(state, roleItemId) {
    state.status = {
      ...state.status,
      itemRemoving: false,
      itemRemoved: true
    };
    state.items = state.items.filter(i => i.roleItemId !== roleItemId);
  },
  removeItemFromRoleFailure(state, error) {
    state.status = {
      ...state.status,
      itemRemoving: false,
      itemRemoved: false
    };
    state.error = error;
  },

  resetState(state) {
    state.status = {};
    state.error = {};
    state.items = [];
  }
};

export const itemsInRole = {
  namespaced: true,
  state,
  actions,
  mutations
};
