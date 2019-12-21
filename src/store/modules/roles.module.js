import { rolesService } from "../../services";
import i18n from "../../localization/i18n";

const state = {
  roles: [],
  role: {},
  status: {},
  error: null,
  roleToAdd: null,
  addEmployeeToRole: {}
};

const actions = {
  getRoles({ commit }) {
    commit("getRolesRequest");

    rolesService.getRoles().then(
      roles => commit("getRolesSuccess", roles),
      error => commit("getRolesFailure", error)
    );
  },
  getRoleById({ commit, dispatch }, roleId) {
    commit("getRoleByIdRequest", roleId);

    rolesService.getRoleById(roleId).then(
      role => commit("getRoleByIdSuccess", role),
      error => {
        commit("getRoleByIdFailure", error);
        dispatch("alert/error", error.toString(), { root: true });
      }
    );
  },
  addRole({ commit, dispatch }, role) {
    commit("addRoleRequest", role);

    rolesService.addRole(role).then(
      role => {
        commit("addRoleSuccess", role);
        dispatch("alert/success", i18n.t("alert.addRoleSuccess"), {
          root: true
        });
      },
      error => {
        commit("addRoleFailure", error);
        dispatch("alert/error", error.toString(), { root: true });
      }
    );
  },
  removeRole({ commit, dispatch }, id) {
    commit("removeRoleRequest", id);

    rolesService.removeRole(id).then(
      () => {
        commit("removeRoleSuccess", id);
        dispatch("alert/success", i18n.t("alert.removeRoleSuccess"), {
          root: true
        });
        dispatch("selectedItems/resetSelectedItems", {}, { root: true });
      },
      error => {
        commit("removeRoleFailure", { id, error: error.toString() });
        dispatch("alert/error", error.toString(), { root: true });
      }
    );
  }
};

const mutations = {
  getRolesRequest(state) {
    state.status = { rolesLoading: true };
  },
  getRolesSuccess(state, roles) {
    state.status = { rolesLoaded: true };
    state.roles = roles;
  },
  getRolesFailure(state, error) {
    state.status = {};
    state.roles = [];
    state.error = error;
  },

  getRoleByIdRequest(state, roleId) {
    state.status = { ...state.status, roleLoading: true };
  },
  getRoleByIdSuccess(state, role) {
    state.status = { ...state.status, roleLoaded: true, roleLoading: false };
    state.role = role;
  },
  getRoleByIdFailure(state, error) {
    state.status = { ...state.status, roleLoaded: false, roleLoading: false };
    state.role = {};
    state.error = {};
  },

  addRoleRequest(state, role) {
    state.roleToAdd = role;
    state.status = { ...state.status, roleAdding: true };
  },
  addRoleSuccess(state, role) {
    state.status = { ...state.status, roleAdded: true, roleAdding: false };
    state.roles.push(role);
  },
  addRoleFailure(state, error) {
    state.status = { ...state.status, roleAdded: false, roleAdding: false };
    state.error = error;
  },

  removeRoleRequest(state, id) {
    state.status = { ...state.status, roleRemoving: true };
    state.roles = state.roles.map(role =>
      role.id === id ? { ...role, roleRemoving: true } : role
    );
  },
  removeRoleSuccess(state, id) {
    state.status = { ...state.status, roleRemoving: false, roleRemoved: true };
    state.roles = state.roles.filter(role => role.id !== id);
  },
  removeRoleFailure(state, { id, error }) {
    state.status = { ...state.status, roleRemoving: false, roleRemoved: false };
    state.roles = state.roles.map(role => {
      if (role.id === id) {
        const { roleRemoving, ...roleCopy } = role;
        return { ...roleCopy, removeError: error };
      }
    });
  }
};

export const roles = {
  namespaced: true,
  state,
  actions,
  mutations
};
