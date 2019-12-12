<template>
  <b-row class="mb-3">
    <b-col>
      <div
        class="card p-2 px-4 role-card-hover pointer"
        style="box-shadow: 0rem 0.1875rem 1.5rem rgba(0, 0, 0, 0.15);"
        @click="handleRoleClick"
      >
        <b-row>
          <b-col cols="auto" v-if="selectionMode" align-self="center">
            <i
              v-if="selectionMode"
              :class="
            `far ${
              selected ? icon.checkedSquare : icon.square
            } fa-lg`
          "
            ></i>
          </b-col>
          <b-col class="auto" align-self="center">
            <h3 class="mt-2">{{ name }}</h3>
          </b-col>
          <b-col align-self="center" class="text-right">
            <i class="fas fa-chevron-down"></i>
          </b-col>
        </b-row>
      </div>
      <b-collapse :id="`collapse-${roleId}`" v-model="collapseVisible" class="mt-2">
        <b-card>
          <p class="card-text">Description: {{ description }}</p>
          <hr />
          <b-row>
            <b-col>
              <base-button
                v-if="!selectionMode"
                type="primary"
                @click="showEmployeesClick"
              >Show employees</base-button>
            </b-col>
          </b-row>
        </b-card>
      </b-collapse>
      <b-collapse
        :id="`employees-collapse-${roleId}`"
        v-model="employeesCollapseVisible"
        class="mt-2"
      >
        <b-card>
          <template v-slot:header>
            <b-row>
              <b-col>
                <form
                  class="navbar-search navbar-search-light form-inline mr-3 d-none d-md-flex ml-lg-auto"
                >
                  <div class="form-group mb-0">
                    <base-input
                      placeholder="Search"
                      class="input-group-alternative"
                      alternative
                      addon-right-icon="fas fa-search"
                    ></base-input>
                  </div>
                </form>
              </b-col>
              <b-col cols="auto" align-self="center">
                <base-button
                  v-if="employeesSelectionMode"
                  type="link"
                  @click="resetEmployeesClick"
                >Reset</base-button>
                <base-button type="success" size="sm" icon="fas fa-plus" @click="addEmployeesClick"></base-button>
                <base-button
                  type="danger"
                  size="sm"
                  icon="fas fa-minus"
                  @click="removeEmployeesClick"
                ></base-button>
              </b-col>
            </b-row>
          </template>

          <div
            class="card p-2 px-4 mb-2 role-card-hover"
            style="background-color: #FFFAFA;"
            v-for="employee in employeesInRole"
            :key="employee.id"
          >
            <b-row>
              <b-col cols="auto" align-self="center">
                <div class="avatar">
                  <img src="img/theme/team-4-800x800.jpg" class="rounded-circle" />
                </div>
              </b-col>
              <b-col>
                <h3>{{ employee.firstName + " " + employee.lastName }}</h3>
                <div class="h5 font-weight-300">{{ employee.login }}</div>
              </b-col>
            </b-row>
          </div>
        </b-card>
      </b-collapse>
    </b-col>
  </b-row>
</template>
<script>
import { mapActions, mapState } from "vuex";

export default {
  name: "role-card",
  props: {
    roleId: Number,
    description: String,
    name: String,
    selectionMode: Boolean
  },
  computed: {
    ...mapState({
      isSelectionReseted: state => state.selectedItems.isSelectedItemsReseted,
      employeesInRole: state => state.employeesInRole.employeesInRole,
      status: state => state.employeesInRole.status
    })
  },
  watch: {
    isSelectionReseted(newV, oldV) {
      if (newV) this.selected = false;
    }
  },
  data() {
    return {
      icon: {
        checkedSquare: "fa-check-square",
        square: "fa-square"
      },
      selected: false,
      collapseVisible: false,
      employeesCollapseVisible: false,
      employeesSelectionMode: false
    };
  },
  methods: {
    ...mapActions("selectedItems", ["addSelectedItem", "removeSelectedItem"]),
    ...mapActions("employeesInRole", [
      "getEmployeesInRole",
      "addEmployeeToRole",
      "RemoveEmployeeFromRole"
    ]),
    handleRoleClick() {
      if (this.selectionMode) {
        this.selected = !this.selected;
        this.selected
          ? this.addSelectedItem(this.roleId)
          : this.removeSelectedItem(this.roleId);
      } else {
        if (this.collapseVisible) this.employeesCollapseVisible = false;
        this.collapseVisible = !this.collapseVisible;
      }
    },
    showEmployeesClick() {
      if (!this.employeesCollapseVisible) {
        this.getEmployeesInRole(this.roleId);
      }
      this.employeesCollapseVisible = !this.employeesCollapseVisible;
    },
    addEmployesClick() {},
    removeEmployeesClick() {},
    resetEmployeesClick() {}
  }
};
</script>