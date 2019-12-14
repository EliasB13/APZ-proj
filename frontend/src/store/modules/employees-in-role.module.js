import { rolesService } from "../../services";

const state = {
  roles: [],
  error: null
};

const actions = {
  getEmployeesInRole({ commit }, roleId) {
    commit("getEmployeesByRoleRequest", roleId);

    rolesService.getEmployeesByRole(roleId).then(
      employees => {
        commit("getEmployeesByRoleSuccess", { employees, roleId });
      },
      error => commit("getEmployeesByRole", { error, roleId })
    );
  },
  addEmployeeToRole({ commit, dispatch }, { emplId, roleId }) {
    commit("addEmployeeToRoleRequest", { emplId, roleId });

    rolesService.addEmployeeToRole(emplId, roleId).then(
      employee => {
        commit("addEmployeeToRoleSuccess", { employee, roleId });
        dispatch("alert/success", "Employee was added to role", { root: true });
      },
      error => {
        commit("addEmployeeToRoleFailure", { error, roleId });
        dispatch("alert/error", "Employee wasn't added to role", {
          root: true
        });
      }
    );
  },
  removeEmployeeFromRole({ commit, dispatch }, { emplId, roleId }) {
    commit("removeEmployeeFromRoleRequest", { emplId, roleId });

    rolesService.removeEmployeeFromRole(emplId, roleId).then(
      () => {
        commit("removeEmployeeFromRoleSuccess", { emplId, roleId });
        dispatch("alert/success", "Employee was removed from role", {
          root: true
        });
        dispatch("selectedItems/resetSelectedItems", {}, { root: true });
      },
      error => {
        commit("removeEmployeeFromRoleFailure", {
          emplId,
          roleId,
          error: error.toString()
        });
        dispatch("alert/error", error, { root: true });
      }
    );
  }
};

const mutations = {
  getEmployeesByRoleRequest(state, roleId) {
    var roleL = state.roles.find(r => r.roleId == roleId);
    if (!roleL) {
      state.roles.push({ roleId, status: { employeesLoading: true } });
    }
    state.roles = state.roles.map(r => {
      if (r.roleId == roleId)
        return {
          ...r,
          status: {
            ...r.status,
            employeesLoading: true
          }
        };
      return r;
    });
  },
  getEmployeesByRoleSuccess(state, role) {
    state.roles = state.roles.map(r => {
      if (r.roleId === role.roleId)
        return {
          ...r,
          status: {
            ...r.status,
            employeesLoading: false,
            employeesLoaded: true
          },
          employees: role.employees
        };
      return r;
    });
  },
  getEmployeesByRoleFailure(state, role) {
    state.roles = state.roles.map(r => {
      if (r.roleId === role.roleId)
        return {
          ...r,
          status: {},
          employees: []
        };
      return r;
    });
    state.error = role.error;
  },

  addEmployeeToRoleRequest(state, role) {
    state.roles = state.roles.map(r => {
      if (r.roleId === role.roleId)
        return {
          ...r,
          status: { ...r.status, employeeToRoleAdding: true },
          employeeToAdd: { roleId: role.roleId, employeeId: role.emplId }
        };
      return r;
    });
  },
  addEmployeeToRoleSuccess(state, role) {
    state.roles = state.roles.map(r => {
      r.employees.push(role.employee);
      if (r.roleId === role.roleId)
        return {
          ...r,
          status: {
            ...r.status,
            employeeToRoleAdding: false,
            employeeToRoleAdded: true
          }
        };
      return r;
    });
  },
  addEmployeeToRoleFailure(state, role) {
    state.roles = state.roles.map(r => {
      if (r.roleId === role.roleId)
        return {
          ...r,
          status: {},
          employeeToAdd: {}
        };
      return r;
    });
    state.error = role.error;
  },

  removeEmployeeFromRoleRequest(state, role) {
    state.roles = state.roles.map(r => {
      if (r.roleId === role.roleId)
        return {
          ...r,
          status: { ...r.status, removingEmployeeFromRole: true }
        };
      return r;
    });
  },
  removeEmployeeFromRoleSuccess(state, role) {
    state.roles = state.roles.map(r => {
      if (r.roleId === role.roleId)
        return {
          ...r,
          status: {
            ...r.status,
            removingEmployeeFromRole: false,
            removedEmployeeFromRole: true
          },
          employees: r.employees.filter(e => e.employeeId !== role.emplId)
        };
      return r;
    });
  },
  removeEmployeeFromRoleFailure(state, role) {
    state.roles = state.roles.map(r => {
      if (r.roleId === role.roleId)
        return {
          ...r,
          status: {
            ...r.status,
            removingEmployeeFromRole: false,
            removedEmployeeFromRole: false
          }
        };
      return r;
    });
    state.error = role.error;
  }
};

export const employeesInRole = {
  namespaced: true,
  state,
  actions,
  mutations
};
