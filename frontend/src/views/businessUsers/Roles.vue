<template>
  <div>
    <base-header type="gradient-success" class="pb-6 pb-8 pt-5 pt-md-8">
      <div class="container-fluid d-flex align-items-center">
        <div class="row">
          <div class="col-lg-7 col-md-10">
            <p class="text-white mt-0 mb-5">
              {{ $t("rolesPage.secondaryHeader") }}
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
                {{
                  selectionMode
                    ? $t("rolesPage.manageBar.remove")
                    : $t("rolesPage.manageBar.manage")
                }}
              </h2>
            </b-col>
            <b-col cols="auto" class="p-3">
              <base-button
                v-if="!selectionMode"
                class="float-right"
                type="success"
                icon="ni ni-fat-add"
                @click="showAddingModal = true"
                >{{ $t("common.addBtn") }}</base-button
              >
              <base-button
                v-if="selectionMode"
                class="float-right"
                type="link"
                @click="resetClick"
                >{{ $t("common.resetBtn") }}</base-button
              >
            </b-col>
            <b-col cols="auto" class="p-3">
              <base-button
                class="float-right"
                type="danger"
                icon="ni ni-fat-remove"
                @click="removeClick"
                >{{ $t("common.removeBtn") }}</base-button
              >
            </b-col>
          </b-row>
        </b-col>
      </b-row>
    </div>

    <div class="container-fluid">
      <div v-if="showRoles">
        <role-card
          v-for="role in roles"
          :key="role.id"
          :selectionMode="selectionMode"
          :roleId="role.id"
          :description="role.description"
          :name="role.name"
        ></role-card>
      </div>
    </div>

    <modal
      :show.sync="showAddingModal"
      header-classes="border"
      footer-classes="border"
      body-classes="p-0"
      modal-classes="modal-dialog-centered modal-sm"
      :showClose="false"
    >
      <div slot="header" class="modal-title">
        {{ $t("rolesPage.modal.header") }}
      </div>
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
              :placeholder="$t('rolesPage.modal.namePlaceholder')"
              :label="$t('rolesPage.modal.nameLabel')"
              v-model="roleToAdd.name"
            ></base-input>
            <base-input
              alternative
              class="mb-3"
              :placeholder="$t('rolesPage.modal.descriptionPlaceholder')"
              :label="$t('rolesPage.modal.descriptionLabel')"
              v-model="roleToAdd.description"
            ></base-input>
          </form>
        </template>
      </card>
      <template slot="footer">
        <base-button type="link" @click="showAddingModal = false">
          {{ $t("common.closeBtn") }}
        </base-button>
        <base-button type="success" class="ml-auto" @click="addItemClick">
          {{ $t("common.addBtn") }}
        </base-button>
      </template>
    </modal>

    <div id="overlay" v-if="showSpinner">
      <b-spinner class="spinner-scaled" label="loading"></b-spinner>
      <br />
      {{ $t("common.spinnerText") }}
    </div>
  </div>
</template>
<script>
import Card from "../../components/Card";
import Modal from "../../components/Modal";
import { mapState, mapActions } from "vuex";

export default {
  components: {
    Card,
    Modal
  },
  data() {
    return {
      showAddingModal: false,
      roleToAdd: {
        name: "",
        description: ""
      },
      selectionMode: false
    };
  },
  computed: {
    ...mapState({
      roles: state => state.roles.roles,
      status: state => state.roles.status,
      selectedRoles: state => state.selectedItems.selectedItems,
      isSelectedItemsReseted: state =>
        state.selectedItems.isSelectedItemsReseted
    }),
    showSpinner() {
      return (
        this.status.rolesLoading ||
        this.status.roleAdding ||
        this.status.roleRemoving
      );
    },
    showRoles() {
      return this.status.rolesLoaded;
    }
  },
  methods: {
    ...mapActions("roles", ["getRoles", "addRole", "removeRole"]),
    ...mapActions("selectedItems", ["resetSelectedItems"]),
    addItemClick() {
      if (this.roleToAdd.name && this.roleToAdd.description) {
        this.addRole(this.roleToAdd);
        this.showAddingModal = false;
      }
    },
    removeClick() {
      if (!this.selectionMode) {
        this.selectionMode = true;
      } else {
        this.selectedRoles.forEach(id => this.removeRole(id));
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
    this.getRoles();
  }
};
</script>
