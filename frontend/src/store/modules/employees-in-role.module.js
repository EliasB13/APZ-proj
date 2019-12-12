import { rolesService } from "../../services";

const state = {
  role: {},
  employeesInRole: [],
  status: {},
  error: null,
  employeeInRoleToAdd: null,
  addEmployeeToRole: {}
};

const actions = {
  getEmployeesInRole({ commit }, roleId) {
    commit("getEmployeesByRoleRequest");

    rolesService.getEmployeesByRole(roleId).then(
      employees => commit("getEmployeesByRoleSuccess", employees),
      error => commit("getEmployeesByRole", error)
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
  getEmployeesByRoleRequest(state, roleId) {
    state.status = { employeesLoading: true };
    state.role = { id: roleId };
  },
  getEmployeesByRoleSuccess(state, employees) {
    state.status = { rolesLoaded: true };
    state.employeesInRole = employees;
  },
  getEmployeesByRoleFailure(state, error) {
    state.status = {};
    state.employeesInRole = [];
    state.error = error;
    state.role = {};
  },

  addEmployeeToRoleRequest(state, emplId, roleId) {
    state.status = { ...state.status, employeeToRoleAdding: true };
    state.addEmployeeToRole = { roleId, emplId };
  },
  addEmployeeToRoleSuccess(state, emplId, roleId) {
    state.status = {
      ...state.status,
      employeeToRoleAdding: false,
      employeeToRoleAdded: true
    };
    state.addEmployeeToRole = { roleId, emplId };
  },
  addEmployeeToRoleFailure(state, error) {
    state.status = {
      ...state.status,
      employeeToRoleAdding: false,
      employeeToRoleAdded: false
    };
    state.error = error;
  },
  removeEmployeeFromRoleRequest(state, emplId, roleId) {
    state.status = { ...state.status, removingEmployeeFromRole: true };
  },
  removeEmployeeFromRoleSuccess(state, emplId, roleId) {
    state.status = {
      ...state.status,
      removingEmployeeFromRole: false,
      removedEmployeeFromRole: true
    };
  },
  removeEmployeeFromRoleFailure(state, emplId, roleId, error) {
    state.status = {
      ...state.status,
      removingEmployeeFromRole: false,
      removedEmployeeFromRole: false
    };
  }
};

export const employeesInRole = {
  namespaced: true,
  state,
  actions,
  mutations
};
