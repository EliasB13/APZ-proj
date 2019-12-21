<template>
  <div>
    <base-header
      type="gradient-success"
      class="header pb-8 pt-5 pt-lg-8 d-flex align-items-center"
    >
      <div class="container-fluid d-flex align-items-center">
        <div class="row">
          <div class="col-lg-7 col-md-10 col-xl-12">
            <h1 class="display-2 text-white mb-5">{{ role.name }}</h1>
          </div>
        </div>
      </div>
    </base-header>

    <div class="container-fluid mt--7 mb-5">
      <b-card>
        <p class="card-text">
          {{ $t("rolePage.description") }}: {{ role.description }}
        </p>
      </b-card>

      <div
        class="card p-2 px-4 role-card-hover pointer mt-5"
        style="box-shadow: 0rem 0.1875rem 1.5rem rgba(0, 0, 0, 0.15);"
        @click="employeesCollapseClick"
      >
        <b-row>
          <b-col cols="auto" align-self="center">
            <i class="ni ni-badge text-success"></i>
          </b-col>
          <b-col cols="auto" class="pl-0" align-self="center">
            <h3 class="mt-2">{{ $t("rolePage.employees") }}</h3>
          </b-col>
          <b-col align-self="center" class="text-right">
            <i class="fas fa-chevron-down"></i>
          </b-col>
        </b-row>
      </div>

      <b-collapse
        :id="`employees-collapse-${roleId}`"
        v-model="showEmployeesCollapse"
        class="mt-2"
      >
        <employees-in-role-list
          :showEmployees="showEmployeesCollapse"
          :roleId="parseInt(roleId)"
        ></employees-in-role-list>
      </b-collapse>

      <div
        class="card p-2 px-4 role-card-hover pointer mt-3"
        style="box-shadow: 0rem 0.1875rem 1.5rem rgba(0, 0, 0, 0.15);"
        @click="itemsCollapseClick"
      >
        <b-row>
          <b-col cols="auto" align-self="center">
            <i class="ni ni-app text-primary mt-1"></i>
          </b-col>
          <b-col cols="auto" class="pl-0" align-self="center">
            <h3 class="mt-2">{{ $t("rolePage.items") }}</h3>
          </b-col>
          <b-col align-self="center" class="text-right">
            <i class="fas fa-chevron-down"></i>
          </b-col>
        </b-row>
      </div>

      <b-collapse
        :id="`items-collapse-${roleId}`"
        v-model="showItemsCollapse"
        class="mt-2"
      >
        <items-in-role-list
          class="mt-2"
          :showItems="showItemsCollapse"
          :roleId="parseInt(roleId)"
        ></items-in-role-list>
      </b-collapse>
    </div>
  </div>
</template>
<script>
import { mapActions, mapState } from "vuex";
import EmployeesInRoleList from "../../components/EmployeesInRoleList";
import ItemsInRoleList from "../../components/ItemsInRoleList";

export default {
  name: "role",
  components: {
    EmployeesInRoleList,
    ItemsInRoleList
  },
  props: {
    roleId: [String, Number]
  },
  data() {
    return {
      showEmployeesCollapse: false,
      showItemsCollapse: false
    };
  },
  computed: {
    ...mapState({
      role: state => state.roles.role,
      roleStatus: state => state.roles.status
    }),
    showSpinner() {
      return this.roleStatus.roleLoading;
    }
  },
  methods: {
    ...mapActions("roles", ["getRoleById"]),
    employeesCollapseClick() {
      this.showEmployeesCollapse = !this.showEmployeesCollapse;
    },
    itemsCollapseClick() {
      this.showItemsCollapse = !this.showItemsCollapse;
    }
  },
  mounted() {
    this.getRoleById(this.roleId);
  }
};
</script>
