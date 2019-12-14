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
                `far ${selected ? icon.checkedSquare : icon.square} fa-lg`
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
              <base-button type="primary" @click="showEmployeesClick">Show employees</base-button>
            </b-col>
          </b-row>
        </b-card>
      </b-collapse>
      <b-collapse
        :id="`employees-collapse-${roleId}`"
        v-model="employeesCollapseVisible"
        class="mt-2"
      >
        <employees-in-role-list :showEmployees="employeesCollapseVisible" :roleId="roleId"></employees-in-role-list>
      </b-collapse>
    </b-col>
  </b-row>
</template>
<script>
import { mapActions, mapState } from "vuex";
import EmployeesInRoleList from "../components/EmployeesInRoleList";

export default {
  name: "role-card",
  components: {
    EmployeesInRoleList
  },
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
      // if (!this.employeesCollapseVisible) {
      //   this.getEmployeesInRole(this.roleId);
      // }
      this.employeesCollapseVisible = !this.employeesCollapseVisible;
    }
  }
};
</script>
