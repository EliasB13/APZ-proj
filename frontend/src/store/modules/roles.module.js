import { rolesService } from "../../services";

const state = {
  roles: [],
  status: {},
  error: null,
  roleToAdd: null,
  addEmployeeToRole: {},
  selectedRoles: []
};

const actions = {
  getRoles({ commit }) {
    commit("getRolesRequest");

    rolesService.getRoles().then(
      roles => commit("getRolesSuccess", roles),
      error => commit("getRolesFailure", error)
    );
  },
  addRole({ commit, dispatch }, role) {
    commit("addRoleRequest", role);

    rolesService.addRole(role).then(
      role => {
        commit("addRoleSuccess", role);
        dispatch("alert/success", "Role was added", { root: true });
      },
      error => {
        commit("addRoleFailure", error);
        dispatch("alert/error", error, { root: true });
      }
    );
  },
  removeRole({ commit, dispatch }, id) {
    commit("removeRoleRequest", id);

    rolesService.removeRole(id).then(
      () => {
        commit("removeRoleSuccess", id);
        dispatch("alert/success", "Role was removed", { root: true });
        dispatch("selectedItems/resetSelectedItems", {}, { root: true });
      },
      error => {
        commit("removeRoleFailure", { id, error: error.toString() });
        dispatch("alert/error", error, { root: true });
      }
    );
  },
  addEmployeeToRole({ commit, dispatch }, emplId, roleId) {
    commit("addEmployeeToRoleRequest", emplId, roleId);

    rolesService.addEmployeeToRole(emplId, roleId).then(
      () => {
        commit("addEmployeeToRoleSuccess", emplId, roleId);
        dispatch("alert/success", "Employee was added to role", { root: true });
      },
      error => {
        commit("addEmployeeToRoleFailure", emplId, roleId, error);
        dispatch("alert/error", "Employee was added to role", { root: true });
      }
    );
  },
  removeEmployeeFromRole({ commit, dispatch }, emplId, roleId) {
    commit("removeEmployeeFromRoleRequest", emplId, roleId);

    rolesService.removeEmployeeFromRole(emplId, roleId).then(
      () => {
        commit("removeEmployeeFromRoleSuccess", emplId, roleId);
        dispatch("alert/success", "Employee was removed from role", {
          root: true
        });
        dispatch("selectedItems/resetSelectedItems", {}, { root: true });
      },
      error => {
        commit(
          "removeEmployeeFromRoleFailure",
          emplId,
          roleId,
          error.toString()
        );
        dispatch("alert/error", error, { root: true });
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

  addRoleRequest(state, role) {
    state.roleToAdd = role;
    state.status.roleAdding = true;
  },
  addRoleSuccess(state, role) {
    state.status.roleAdded = true;
    state.status.roleAdding = false;
    state.roles.push(role);
  },
  addRoleFailure(state, error) {
    state.status.roleAdded = false;
    state.status.roleAdding = false;
    state.error = error;
  },

  removeRoleRequest(state, id) {
    state.status.roleRemoving = true;
    state.roles = state.roles.map(role => {
      role.id === id ? { ...role, roleRemoving: true } : role;
    });
  },
  removeRoleSuccess(state, id) {
    state.status.roleRemoving = false;
    state.status.roleRemoved = true;
    state.roles = state.roles.filter(role => role.id !== id);
  },
  removeRoleFailure(state, { id, error }) {
    state.status.roleRemoving = false;
    state.status.roleRemoved = false;
    state.roles = state.roles.map(role => {
      if (role.id === id) {
        const { roleRemoving, ...roleCopy } = role;
        return { ...roleCopy, removeError: error };
      }
    });
  },

  addEmployeeToRoleRequest(state, emplId, roleId) {
    state.status.employeeToRoleAdding = true;
    state.addEmployeeToRole = { roleId, emplId };
  },
  addEmployeeToRoleSuccess(state, emplId, roleId) {
    state.status.employeeToRoleAdding = false;
    state.status.employeeToRoleAdded = true;
    state.addEmployeeToRole = { roleId, emplId };
  },
  addEmployeeToRoleFailure(state, error) {
    state.status.employeeToRoleAdding = false;
    state.status.employeeToRoleAdded = false;
    state.error = error;
  },
  removeEmployeeFromRoleRequest(state, emplId, roleId) {
    state.status.removingEmployeeFromRole = true;
  },
  removeEmployeeFromRoleSuccess(state, emplId, roleId) {
    state.status.removingEmployeeFromRole = false;
    state.status.removedEmployeeFromRole = true;
  },
  removeEmployeeFromRoleFailure(state, emplId, roleId, error) {
    state.status.removingEmployeeFromRole = false;
    state.status.removedEmployeeFromRole = false;
  }
};

export const roles = {
  namespaced: true,
  state,
  actions,
  mutations
};
