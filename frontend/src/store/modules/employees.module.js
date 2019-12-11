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
        commit("deleteEmployeeSuccess", id);
        commit("removeSelectedItem", id);
        dispatch("alert/success", "Item was added", { root: true });
      },
      error => {
        commit("deleteItemFailure", { id, error: error.toString() });
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
    state.emloyeeToAdd = { login };
    state.status.employeeAdding = true;
  },
  addEmployeeSuccess(state, employee) {
    state.status.employeeAdded = true;
    state.status.employeeAdding = false;
    state.employees.push(employee);
  },
  addEmployeeFailure(state, error) {
    state.status.employeeAdded = false;
    state.status.employeeAdding = false;
  },

  removeEmployeeRequest(state, id) {
    state.status.employeeRemoving = true;
    state.employees = state.employees.map(empl =>
      empl.id === id ? { ...empl, employeeRemoving: true } : empl
    );
  },
  removeEmployeeSuccess(state, id) {
    state.status.employeeRemoved = true;
    state.status.employeeRemoving = false;
    state.employees = state.employees.filter(empl => empl.id !== id);
  },
  removeEmployeeFailure(state, { id, error }) {
    state.status.employeeRemoved = false;
    state.status.employeeRemoving = false;
    state.employees = state.employees.map(empl => {
      if (empl.id === id) {
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
