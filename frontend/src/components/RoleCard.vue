<template>
  <b-row class="mb-3">
    <b-col>
      <div
        class="card p-2 px-4 role-card-hover pointer"
        style="box-shadow: 0rem 0.1875rem 1.5rem rgba(0, 0, 0, 0.15);"
        @click="roleClick(roleId)"
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
        </b-row>
      </div>
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
    roleClick(roleId) {
      this.$router.push({ name: "role", params: { roleId: roleId } });
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
