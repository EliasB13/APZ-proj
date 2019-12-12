import Vue from "vue";
import Vuex from "vuex";

import { alert } from "./modules/alert.module";
import { account } from "./modules/account.module";
import { businessItems } from "./modules/business-items.module";
import { employees } from "./modules/employees.module";
import { selectedItems } from "./modules/selectedItems.module";
import { roles } from "./modules/roles.module";
import { employeesInRole } from "./modules/employees-in-role.module";

Vue.use(Vuex);

export const store = new Vuex.Store({
  modules: {
    alert,
    account,
    selectedItems,
    businessItems,
    employees,
    roles,
    employeesInRole
  }
});
