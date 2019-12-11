<template>
  <div>
    <base-header type="gradient-success" class="pb-6 pb-8 pt-5 pt-md-8">
      <div class="container-fluid d-flex align-items-center">
        <div class="row">
          <div class="col-lg-7 col-md-10">
            <p
              class="text-white mt-0 mb-5"
            >This is your items page. Here you can explore or manage your items</p>
          </div>
        </div>
      </div>
    </base-header>

    <div class="container-fluid mt--7 mb-5">
      <b-row class="px-3">
        <b-col class="manage-bar" style="background-color: white; border-radius: 0.375rem">
          <b-row>
            <b-col align-self="center">
              <h2 class="pl-3">{{ selectionMode ? "Select items for removing" : "Manage" }}</h2>
            </b-col>
            <b-col cols="auto" class="p-3">
              <base-button
                v-if="!selectionMode"
                class="float-right"
                type="success"
                icon="ni ni-fat-add"
                @click="showAddingModal = true"
              >Add</base-button>
              <base-button
                v-if="selectionMode"
                class="float-right"
                type="link"
                @click="resetClick"
              >Reset</base-button>
            </b-col>
            <b-col cols="auto" class="p-3">
              <base-button
                class="float-right"
                type="danger"
                icon="ni ni-fat-remove"
                @click="removeClick"
              >Remove</base-button>
            </b-col>
          </b-row>
        </b-col>
      </b-row>
    </div>

    <div class="container-fluid">
      <base-cards-list
        cardType="base-card"
        :selectionMode="selectionMode"
        v-if="showItems"
        :itemsList="items"
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
              placeholder="Item name"
              label="Name"
              v-model="itemToAdd.name"
            ></base-input>
            <base-input
              alternative
              placeholder="Description"
              label="Description"
              v-model="itemToAdd.description"
            ></base-input>
          </form>
        </template>
      </card>
      <template slot="footer">
        <base-button type="link" @click="showAddingModal = false">Close</base-button>
        <base-button type="success" class="ml-auto" @click="addItemClick">Add</base-button>
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
      itemToAdd: {
        name: "",
        description: ""
      },
      selectionMode: false
    };
  },
  computed: {
    ...mapState({
      items: state => state.businessItems.items,
      status: state => state.businessItems.status,
      selectedItems: state => state.selectedItems.selectedItems,
      isSelectedItemsReseted: state =>
        state.selectedItems.isSelectedItemsReseted
    }),
    showSpinner() {
      return (
        this.status.itemsLoading ||
        this.status.itemAdding ||
        this.status.itemRemoving
      );
    },
    showItems() {
      return this.status.itemsLoaded;
    }
  },
  methods: {
    ...mapActions("businessItems", ["getItems", "addItem", "removeItem"]),
    ...mapActions("selectedItems", ["resetSelectedItems"]),
    addItemClick() {
      if (this.itemToAdd.name && this.itemToAdd.description) {
        this.addItem(this.itemToAdd);
        this.showAddingModal = false;
      }
    },
    removeClick() {
      if (!this.selectionMode) {
        this.selectionMode = true;
      } else {
        this.selectedItems.forEach(id => this.removeItem(id));
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
    this.getItems();
  }
};
</script>
<style>
</style>
