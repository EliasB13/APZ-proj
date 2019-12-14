<template>
  <b-card v-if="showEmployees">
    <template v-slot:header>
      <b-row>
        <b-col>
          <form
            class="navbar-search navbar-search-light form-inline mr-3 d-none d-md-flex ml-lg-auto"
            v-on:submit.prevent="searchSubmit"
          >
            <div class="form-group mb-0">
              <base-input
                placeholder="Search"
                class="input-group-alternative"
                alternative
                addon-right-icon="fas fa-search"
                v-model="employeesSearchQuery"
              ></base-input>
            </div>
          </form>
        </b-col>
        <b-col cols="auto" align-self="center">
          <base-button v-if="employeesSelectionMode" type="link" @click="resetEmployeesClick">Reset</base-button>
          <base-button
            v-if="!employeesSelectionMode"
            type="success"
            size="sm"
            icon="fas fa-plus"
            @click="showAddingModal = true"
          ></base-button>
          <base-button type="danger" size="sm" icon="fas fa-minus" @click="removeEmployeeClick"></base-button>
        </b-col>
      </b-row>
    </template>

    <div v-if="!showSpinner">
      <div
        class="card p-2 px-4 mb-2 role-card-hover bg-secondary"
        v-for="employee in employeesCopy"
        :key="employee.employeeId * 100"
      >
        <div @click="employeeInRoleClick(employee.employeeId)">
          <b-row align-v="center">
            <b-col cols="auto" v-if="employeesSelectionMode">
              <i
                :class="
                      `far ${employee.selected ? icon.checkedSquare : icon.square} fa-lg`
                    "
              ></i>
            </b-col>
            <b-col cols="auto">
              <img src="img/theme/team-4-800x800.jpg" class="rounded-circle avatar" />
            </b-col>
            <b-col>
              <h3 class="mt-1">{{ employee.firstName + " " + employee.lastName }}</h3>
              <div class="h5 font-weight-300 mb-1">{{ employee.login }}</div>
            </b-col>
          </b-row>
        </div>
      </div>
    </div>

    <modal
      :show.sync="showAddingModal"
      header-classes="border"
      footer-classes="border"
      body-classes="p-0"
      modal-classes="modal-dialog-centered modal-lg"
      :showClose="false"
    >
      <div slot="header" class="modal-title">Select employee to add in role</div>
      <card
        type="secondary"
        header-classes="bg-white text-default"
        body-classes="px-lg-5 py-lg-5"
        class="border-0"
      >
        <template>
          <div
            class="navbar-search navbar-search-light form-inline d-none d-md-flex ml-lg-auto mb-4"
            v-on:submit.prevent
          >
            <div class="form-group mb-0">
              <base-input
                placeholder="Search"
                class="input-group-alternative"
                alternative
                addon-right-icon="fas fa-search"
                v-model="modalEmployeesSearchQuery"
              ></base-input>
            </div>
          </div>
          <div class="scrollable-list">
            <div
              class="card p-2 px-4 mb-2 role-card-hover pointer"
              v-for="mEmployee in modalEmplpoyeesCopy"
              :key="mEmployee.employeeId"
            >
              <div @click="modalEmployeeClick(mEmployee.employeeId)">
                <b-row align-v="center">
                  <b-col cols="auto">
                    <i
                      :class="
                      `far ${mEmployee.selected ? icon.checkedSquare : icon.square} fa-lg`
                    "
                    ></i>
                  </b-col>
                  <b-col cols="auto">
                    <img src="img/theme/team-4-800x800.jpg" class="rounded-circle avatar" />
                  </b-col>
                  <b-col>
                    <h3 class="mt-1">{{ mEmployee.firstName + " " + mEmployee.lastName }}</h3>
                    <div class="h5 font-weight-300 mb-1">{{ mEmployee.login }}</div>
                  </b-col>
                </b-row>
              </div>
            </div>
          </div>
          <div v-if="showModalSpinner" class="text-center">
            <b-spinner class="spinner-scaled" label="loading"></b-spinner>
            <br />Loading
          </div>
        </template>
      </card>
      <template slot="footer">
        <base-button type="link" @click="showAddingModal = false">Close</base-button>
        <base-button type="success" class="ml-auto" @click="addEmployeeClick">Add</base-button>
      </template>
    </modal>
    <div v-if="showSpinner" class="text-center">
      <b-spinner class="spinner-scaled" label="loading"></b-spinner>
      <br />Loading
    </div>
  </b-card>
</template>
<script>
import { mapActions, mapState } from "vuex";

export default {
  name: "employees-in-role-list",
  props: {
    showEmployees: Boolean,
    roleId: Number
  },
  computed: {
    ...mapState({
      modalEmployees: state => state.employees.employees,
      modalEmployeestatus: state => state.employees.status,
      isSelectionReseted: state => state.selectedItems.isSelectedItemsReseted,
      roles: state => state.employeesInRole.roles
    }),
    showSpinner() {
      var role = this.roles.find(e => e.roleId == this.roleId);
      return (
        role &&
        (role.status.employeesLoading ||
          role.status.employeeToRoleAdding ||
          role.status.removingEmployeeFromRole)
      );
    },
    showModalSpinner() {
      return this.modalEmployeestatus.employeesLoading;
    },
    showModalEmployees() {
      return this.modalEmployeestatus.employeesLoaded;
    }
  },
  watch: {
    isSelectionReseted(newV, oldV) {
      if (newV) this.selected = false;
    },
    showEmployees(newV, oldV) {
      if (newV) this.getEmployeesInRole(this.roleId);
      else this.employeesSelectionMode = false;
    },
    roles(newV, oldV) {
      newV.forEach(r => {
        if (r.roleId == this.roleId && r.employees) {
          this.employees = r.employees;
          this.employeesCopy = r.employees;
        }
      });
    },
    modalEmployees(newV, oldV) {
      this.modalEmplpoyeesCopy = this.modalEmployees;
    },
    employeesSearchQuery(newV, oldV) {
      if (newV !== "") {
        this.employeesCopy = this.employees.filter(e =>
          e.login.toLowerCase().includes(newV.toLowerCase())
        );
      } else {
        this.employeesCopy = this.employees;
      }
    },
    modalEmployeesSearchQuery(newV, oldV) {
      if (newV !== "") {
        this.modalEmplpoyeesCopy = this.modalEmployees.filter(e =>
          e.login.toLowerCase().includes(newV.toLowerCase())
        );
      } else {
        this.modalEmplpoyeesCopy = this.modalEmployees;
      }
    },
    showAddingModal(newV, oldV) {
      if (newV) {
        this.getEmployees();
      }
    }
  },
  data() {
    return {
      icon: {
        checkedSquare: "fa-check-square",
        square: "fa-square"
      },
      employeesSelectionMode: false,
      employees: [],
      employeesCopy: [],
      showAddingModal: false,
      employeesSearchQuery: "",
      modalEmployeesSearchQuery: "",
      employeeToAdd: {
        login: ""
      },
      modalEmplpoyeesCopy: []
    };
  },
  methods: {
    ...mapActions("selectedItems", ["addSelectedItem", "removeSelectedItem"]),
    ...mapActions("employeesInRole", [
      "getEmployeesInRole",
      "addEmployeeToRole",
      "removeEmployeeFromRole"
    ]),

    ...mapActions("employees", ["getEmployees"]),
    addEmployeeClick() {
      this.modalEmplpoyeesCopy.forEach(e => {
        if (e.selected)
          this.addEmployeeToRole({ emplId: e.employeeId, roleId: this.roleId });
      });
      this.showAddingModal = false;
    },
    removeEmployeeClick() {
      if (!this.employeesSelectionMode) this.employeesSelectionMode = true;
      else
        this.employeesCopy.forEach(e => {
          if (e.selected)
            this.removeEmployeeFromRole({
              emplId: e.employeeId,
              roleId: this.roleId
            });
        });
    },
    resetEmployeesClick() {
      this.employeesSelectionMode = false;
    },
    searchSubmit() {},
    modalEmployeeClick(emplId) {
      this.modalEmplpoyeesCopy = this.modalEmplpoyeesCopy.slice().map(me => {
        if (me.employeeId == emplId) {
          if (me.selected == undefined) {
            return {
              ...me,
              selected: true
            };
          } else {
            return {
              ...me,
              selected: me.selected ? false : true
            };
          }
        }
        return me;
      });
    },
    employeeInRoleClick(emplId) {
      this.employeesCopy = this.employeesCopy.slice().map(me => {
        if (me.employeeId == emplId) {
          if (me.selected == undefined) {
            return {
              ...me,
              selected: true
            };
          } else {
            return {
              ...me,
              selected: me.selected ? false : true
            };
          }
        }
        return me;
      });
    }
  }
};
</script>
<style>
.scrollable-list {
  max-height: 30rem;
  overflow-y: auto;
}
</style>