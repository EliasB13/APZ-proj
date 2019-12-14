<template>
  <div>
    <base-header type="gradient-success" class="pb-6 pb-8 pt-5 pt-md-8">
      <div class="container-fluid d-flex align-items-center">
        <div class="row">
          <div class="col-lg-7 col-md-10">
            <p class="text-white mt-0 mb-5">
              This is your consumers page. Here you can explore or manage
              customers of your business.
            </p>
          </div>
        </div>
      </div>
    </base-header>

    <div class="container-fluid mt--7 mb-5">
      <b-row class="px-3">
        <b-col
          class="manage-bar"
          style="background-color: white; border-radius: 0.375rem"
        >
          <b-row>
            <b-col align-self="center">
              <h2 class="pl-3">
                {{ selectionMode ? "Select employees for removing" : "Manage" }}
              </h2>
            </b-col>
            <b-col cols="auto" class="p-3">
              <base-button
                v-if="!selectionMode"
                class="float-right"
                type="success"
                icon="ni ni-fat-add"
                @click="showAddingModal = true"
                >Add</base-button
              >
              <base-button
                v-if="selectionMode"
                class="float-right"
                type="link"
                @click="resetClick"
                >Reset</base-button
              >
            </b-col>
            <b-col cols="auto" class="p-3">
              <base-button
                class="float-right"
                type="danger"
                icon="ni ni-fat-remove"
                @click="removeClick"
                >Remove</base-button
              >
            </b-col>
          </b-row>
        </b-col>
      </b-row>
    </div>

    <div class="container-fluid">
      <base-cards-list
        class="mt-7"
        cardType="user-card"
        :selectionMode="selectionMode"
        v-if="showEmployees"
        :itemsList="employees"
      ></base-cards-list>
    </div>

    <modal
      :show.sync="showAddingModal"
      header-classes="border"
      footer-classes="border"
      body-classes="p-0"
      modal-classes="modal-dialog-centered modal-sm"
      :showClose="false"
    >
      <div slot="header" class="modal-title">Add new item</div>
      <card
        type="secondary"
        header-classes="bg-white text-default"
        body-classes="px-lg-5 py-lg-5"
        class="border-0"
      >
        <template>
          <form role="form">
            <base-input
              alternative
              class="mb-3"
              placeholder="Login"
              label="Employee login"
              v-model="employeeToAdd.login"
            ></base-input>
          </form>
        </template>
      </card>
      <template slot="footer">
        <base-button type="link" @click="showAddingModal = false"
          >Close</base-button
        >
        <base-button type="success" class="ml-auto" @click="addItemClick"
          >Add</base-button
        >
      </template>
    </modal>

    <div id="overlay" v-if="showSpinner">
      <b-spinner class="spinner-scaled" label="loading"></b-spinner>
      <br />Loading
    </div>
  </div>
</template>
<script>
import BaseCardsList from "../../components/BaseCardsList";
import Card from "../../components/Card";
import Modal from "../../components/Modal";
import { mapState, mapActions } from "vuex";

export default {
  components: {
    BaseCardsList,
    Card,
    Modal
  },
  data() {
    return {
      showAddingModal: false,
      employeeToAdd: {
        login: ""
      },
      selectionMode: false
    };
  },
  computed: {
    ...mapState({
      employees: state => state.employees.employees,
      status: state => state.employees.status,
      selectedEmployees: state => state.selectedItems.selectedItems,
      isSelectedItemsReseted: state =>
        state.selectedItems.isSelectedItemsReseted
    }),
    showSpinner() {
      return (
        this.status.employeesLoading ||
        this.status.employeeAdding ||
        this.status.employeeRemoving
      );
    },
    showEmployees() {
      return this.status.employeesLoaded;
    }
  },
  methods: {
    ...mapActions("employees", [
      "getEmployees",
      "addEmployee",
      "removeEmployee"
    ]),
    ...mapActions("selectedItems", ["resetSelectedItems"]),
    addItemClick() {
      if (this.employeeToAdd.login) {
        this.addEmployee(this.employeeToAdd.login);
        this.showAddingModal = false;
      }
    },
    removeClick() {
      if (!this.selectionMode) {
        this.selectionMode = true;
      } else {
        this.selectedEmployees.forEach(id => this.removeEmployee(id));
      }
    },
    resetClick() {
      if (this.selectionMode) {
        this.selectionMode = false;
        this.resetSelectedItems();
      }
    }
  },
  mounted() {
    this.getEmployees();
  }
};
</script>
<style></style>
