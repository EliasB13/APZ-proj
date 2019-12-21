import { rolesService } from "../../services";
import i18n from "../../localization/i18n";

const state = {
  employees: [],
  status: {},
  employeesToAdd: {},
  error: null
};

const actions = {
  getEmployeesInRole({ commit, dispatch }, roleId) {
    commit("getEmployeesByRoleRequest", roleId);

    rolesService.getEmployeesByRole(roleId).then(
      employees => {
        commit("getEmployeesByRoleSuccess", employees);
      },
      error => {
        commit("getEmployeesByRole", error);
        dispatch("alert/error", error.toString(), {
          root: true
        });
      }
    );
  },
  addEmployeeToRole({ commit, dispatch }, { emplId, roleId }) {
    commit("addEmployeeToRoleRequest", { emplId, roleId });

    rolesService.addEmployeeToRole(emplId, roleId).then(
      employee => {
        commit("addEmployeeToRoleSuccess", employee);
        dispatch("alert/success", i18n.t("alert.addEmployeeToRoleSuccess"), {
          root: true
        });
      },
      error => {
        commit("addEmployeeToRoleFailure", error);
        dispatch("alert/error", error.toString(), {
          root: true
        });
      }
    );
  },
  removeEmployeeFromRole({ commit, dispatch }, { emplId, roleId }) {
    commit("removeEmployeeFromRoleRequest", { emplId, roleId });

    rolesService.removeEmployeeFromRole(emplId, roleId).then(
      () => {
        commit("removeEmployeeFromRoleSuccess", emplId);
        dispatch(
          "alert/success",
          i18n.t("alert.removeEmployeeFromRoleSuccess"),
          {
            root: true
          }
        );
        dispatch("selectedItems/resetSelectedItems", {}, { root: true });
      },
      error => {
        commit("removeEmployeeFromRoleFailure", error);
        dispatch("alert/error", error.toString(), { root: true });
      }
    );
  },
  resetState({ commit }) {
    commit("resetState");
  }
};

const mutations = {
  getEmployeesByRoleRequest(state, roleId) {
    state.status = { ...state.status, employeesLoading: true };
  },
  getEmployeesByRoleSuccess(state, employees) {
    state.status = {
      ...state.status,
      employeesLoading: false,
      employeesLoaded: true
    };
    state.employees = employees;
  },
  getEmployeesByRoleFailure(state, error) {
    state.status = {
      ...state.status,
      employeesLoading: false,
      employeesLoaded: false
    };
    state.employees = [];
    state.error = error;
  },

  addEmployeeToRoleRequest(state, empl) {
    state.status = {
      ...state.status,
      employeeAdding: true
    };
    state.employeeToAdd = empl;
  },
  addEmployeeToRoleSuccess(state, empl) {
    state.status = {
      ...state.status,
      employeeAdding: false,
      employeeAdded: false
    };
    state.employees.push(empl);
  },
  addEmployeeToRoleFailure(state, error) {
    state.status = {
      ...state.status,
      employeeAdding: false,
      employeeAdded: false
    };
    state.error = error;
  },

  removeEmployeeFromRoleRequest(state, empl) {
    state.status = {
      ...state.status,
      employeeRemoving: true
    };
  },
  removeEmployeeFromRoleSuccess(state, emplId) {
    state.status = {
      ...state.status,
      employeeRemoving: false,
      employeeRemoved: true
    };
    state.employees = state.employees.filter(e => e.employeeId !== emplId);
  },
  removeEmployeeFromRoleFailure(state, error) {
    state.status = {
      ...state.status,
      employeeRemoving: false,
      employeeRemoved: false
    };
    state.error = error;
  },

  resetState(state) {
    state.status = {};
    state.error = {};
    state.employees = [];
  }
};

export const employeesInRole = {
  namespaced: true,
  state,
  actions,
  mutations
};
