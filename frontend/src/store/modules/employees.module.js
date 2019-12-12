import { employeesService } from "../../services";

const state = {
  employees: [],
  status: {},
  error: null,
  emloyeeToAdd: null,
  selectedEmployees: []
};

const actions = {
  getEmployees({ commit }) {
    commit("getEmployeesRequest");

    employeesService.getEmployees().then(
      employees => commit("getEmployeesSuccess", employees),
      error => commit("getItemsFailure", error)
    );
  },
  addEmployee({ commit, dispatch }, login) {
    commit("addEmployeeRequest", login);

    employeesService.addEmployee(login).then(
      employee => {
        commit("addEmployeeSuccess", employee);
        dispatch("alert/success", "Employee was added", { root: true });
      },
      error => {
        commit("addEmployeeFailure", error);
        dispatch("alert/error", error, { root: true });
      }
    );
  },
  removeEmployee({ commit, dispatch }, id) {
    commit("removeEmployeeRequest", id);

    employeesService.removeEmployee(id).then(
      () => {
        commit("removeEmployeeSuccess", id);
        dispatch("alert/success", "Employee was removed", { root: true });
        dispatch("selectedItems/resetSelectedItems", {}, { root: true });
      },
      error => {
        commit("removeEmployeeFailure", { id, error: error.toString() });
        dispatch("alert/error", error, { root: true });
      }
    );
  }
};

const mutations = {
  getEmployeesRequest(state) {
    state.status = { employeesLoading: true };
  },
  getEmployeesSuccess(state, employees) {
    state.status = { employeesLoaded: true };
    state.employees = employees;
  },
  getEmployeesFailure(state, error) {
    state.status = {};
    state.employees = [];
    state.error = error;
  },

  addEmployeeRequest(state, login) {
    state.status = { ...state.status, employeeAdding: true };
    state.emloyeeToAdd = { login };
  },
  addEmployeeSuccess(state, employee) {
    state.status = {
      ...state.status,
      employeeAdding: false,
      employeeAdded: true
    };
    state.employees.push(employee);
  },
  addEmployeeFailure(state, error) {
    state.status = {
      ...state.status,
      employeeAdding: false,
      employeeAdded: false
    };
    state.error = error;
  },

  removeEmployeeRequest(state, id) {
    state.status = { ...state.status, employeeRemoving: true };
    state.employees = state.employees.map(empl =>
      empl.employeeId === id ? { ...empl, employeeRemoving: true } : empl
    );
  },
  removeEmployeeSuccess(state, id) {
    state.status = {
      ...state.status,
      employeeRemoving: false,
      employeeRemoved: true
    };
    state.employees = state.employees.filter(empl => empl.employeeId !== id);
  },
  removeEmployeeFailure(state, { id, error }) {
    state.status = {
      ...state.status,
      employeeRemoving: false,
      employeeRemoved: false
    };
    state.employees = state.employees.map(empl => {
      if (empl.employeeId === id) {
        const { employeeRemoving, ...emplCopy } = empl;
        return { ...emplCopy, removeError: error };
      }
    });
  }
};

export const employees = {
  namespaced: true,
  state,
  actions,
  mutations
};
