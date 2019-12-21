<template>
  <b-card>
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
                v-model="itemsSearchQuery"
              ></base-input>
            </div>
          </form>
        </b-col>
        <b-col cols="auto" align-self="center">
          <base-button
            v-if="itemsSelectionMode"
            type="link"
            @click="resetItemsClick"
            >Reset</base-button
          >
          <base-button
            v-if="!itemsSelectionMode"
            type="success"
            size="sm"
            icon="fas fa-plus"
            @click="showAddingModal = true"
          ></base-button>
          <base-button
            type="danger"
            size="sm"
            icon="fas fa-minus"
            @click="removeItemClick"
          ></base-button>
        </b-col>
      </b-row>
    </template>

    <div v-if="!showSpinner">
      <div
        class="card p-2 px-4 mb-2 role-card-hover bg-secondary"
        v-for="item in itemsCopy"
        :key="item.sharedItemId"
      >
        <div @click="itemInRoleClick(item.sharedItemId)">
          <b-row align-v="center">
            <b-col cols="auto" v-if="itemsSelectionMode">
              <i
                :class="
                  `far ${
                    item.selected ? icon.checkedSquare : icon.square
                  } fa-lg`
                "
              ></i>
            </b-col>
            <b-col cols="auto">
              <b-img class="thumbnail-img" rounded src="/img/theme/item.png" />
            </b-col>
            <b-col>
              <h3 class="mt-1">{{ item.name }}</h3>
              <div class="h5 font-weight-300 mb-1">{{ item.description }}</div>
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
      <div slot="header" class="modal-title">Select item to add in role</div>
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
                v-model="modalItemsSearchQuery"
              ></base-input>
            </div>
          </div>
          <div class="scrollable-list">
            <div
              class="card p-2 px-4 mb-2 role-card-hover pointer"
              v-for="mItem in modalItemsCopy"
              :key="mItem.id"
            >
              <div @click="modalItemClick(mItem.id)">
                <b-row align-v="center">
                  <b-col cols="auto">
                    <i
                      :class="
                        `far ${
                          mItem.selected ? icon.checkedSquare : icon.square
                        } fa-lg`
                      "
                    ></i>
                  </b-col>
                  <b-col cols="auto">
                    <b-img
                      class="thumbnail-img"
                      rounded
                      src="/img/theme/item.png"
                    />
                  </b-col>
                  <b-col>
                    <h3 class="mt-1">{{ mItem.name }}</h3>
                    <div class="h5 font-weight-300 mb-1">
                      {{ mItem.description }}
                    </div>
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
        <base-button type="link" @click="showAddingModal = false"
          >Close</base-button
        >
        <base-button type="success" class="ml-auto" @click="addItemClick"
          >Add</base-button
        >
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
  name: "items-in-role-list",
  props: {
    showItems: Boolean,
    roleId: Number
  },
  computed: {
    ...mapState({
      modalItems: state => state.businessItems.items,
      modalItemstatus: state => state.businessItems.status,
      isSelectionReseted: state => state.selectedItems.isSelectedItemsReseted,
      items: state => state.itemsInRole.items,
      status: state => state.itemsInRole.status
    }),
    showSpinner() {
      return (
        this.status.itemsLoading ||
        this.status.itemAdding ||
        this.status.itemRemoving
      );
    },
    showModalSpinner() {
      return this.modalItemstatus.itemsLoading;
    },
    showModalItems() {
      return this.modalItemstatus.itemsLoaded;
    }
  },
  watch: {
    isSelectionReseted(newV, oldV) {
      if (newV) this.selected = false;
    },
    showItems(newV, oldV) {
      if (newV == true) {
        if (!this.status.itemsLoaded) this.getItemsInRole(this.roleId);
      } else this.itemsSelectionMode = false;
    },
    items(newV, oldV) {
      if (newV) this.itemsCopy = this.items;
    },
    modalItems(newV, oldV) {
      if (newV) this.modalItemsCopy = this.modalItems;
    },
    itemsSearchQuery(newV, oldV) {
      if (newV !== "") {
        this.itemsCopy = this.items.filter(i =>
          i.name.toLowerCase().includes(newV.toLowerCase())
        );
      } else {
        this.itemsCopy = this.items;
      }
    },
    modalItemsSearchQuery(newV, oldV) {
      if (newV !== "") {
        this.modalItemsCopy = this.modalItems.filter(i =>
          i.name.toLowerCase().includes(newV.toLowerCase())
        );
      } else {
        this.modalItemsCopy = this.modalItems;
      }
    },
    showAddingModal(newV, oldV) {
      if (newV) {
        this.getItems();
      }
    },
    status(newV) {
      if (newV.itemRemoved) this.itemsSelectionMode = false;
    }
  },
  data() {
    return {
      icon: {
        checkedSquare: "fa-check-square",
        square: "fa-square"
      },
      itemsSelectionMode: false,
      itemsCopy: [],
      showAddingModal: false,
      itemsSearchQuery: "",
      modalItemsSearchQuery: "",
      modalItemsCopy: []
    };
  },
  methods: {
    ...mapActions("selectedItems", ["addSelectedItem", "removeSelectedItem"]),
    ...mapActions("itemsInRole", [
      "getItemsInRole",
      "addItemToRole",
      "removeItemFromRole",
      "resetState"
    ]),

    ...mapActions("businessItems", ["getItems"]),
    addItemClick() {
      this.modalItemsCopy.forEach(i => {
        if (i.selected)
          this.addItemToRole({ itemId: i.id, roleId: this.roleId });
      });
      this.showAddingModal = false;
    },
    removeItemClick() {
      if (!this.itemsSelectionMode) this.itemsSelectionMode = true;
      else
        this.itemsCopy.forEach(i => {
          if (i.selected) this.removeItemFromRole(i.roleItemId);
        });
    },
    resetItemsClick() {
      this.itemsSelectionMode = false;
    },
    modalItemClick(itemId) {
      this.modalItemsCopy = this.modalItemsCopy.slice().map(mi => {
        if (mi.id == itemId) {
          if (mi.selected == undefined) {
            return {
              ...mi,
              selected: true
            };
          } else {
            return {
              ...mi,
              selected: mi.selected ? false : true
            };
          }
        }
        return mi;
      });
    },
    itemInRoleClick(itemId) {
      if (this.itemsSelectionMode) {
        this.itemsCopy = this.itemsCopy.slice().map(mi => {
          if (mi.sharedItemId == itemId) {
            if (mi.selected == undefined) {
              return {
                ...mi,
                selected: true
              };
            } else {
              return {
                ...mi,
                selected: mi.selected ? false : true
              };
            }
          }
          return mi;
        });
      }
    }
  },
  mounted() {
    this.resetState();
  }
};
</script>
<style>
.scrollable-list {
  max-height: 30rem;
  overflow-y: auto;
}
</style>
