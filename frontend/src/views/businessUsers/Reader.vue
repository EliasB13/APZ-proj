<template>
  <div>
    <base-header type="gradient-success" class="header pb-8 pt-5 pt-lg-8 d-flex align-items-center">
      <div class="container-fluid d-flex align-items-center">
        <div class="row">
          <div class="col-lg-7 col-md-10 col-xl-12">
            <h1 class="display-2 text-white mb-5">{{ $t("readersPage.reader") + " No " + readerId }}</h1>
          </div>
        </div>
      </div>
    </base-header>

    <div class="container-fluid mt--7 mb-5">
      <b-card>
        <p
          class="card-text"
        >{{ $t("rolePage.description") }}: {{ $t("readersPage.reader") + " No " + readerId }}</p>
      </b-card>

      <div
        class="card p-2 px-4 role-card-hover pointer mt-5"
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

      <b-collapse :id="`items-collapse-${readerId}`" v-model="showItemsCollapse" class="mt-2">
        <items-in-reader-list :showItems="showItemsCollapse" :readerId="parseInt(readerId)"></items-in-reader-list>
      </b-collapse>
    </div>
  </div>
</template>
<script>
import ItemsInReaderList from "../../components/ItemsInReaderList";

export default {
  name: "reader",
  components: {
    ItemsInReaderList
  },
  props: {
    readerId: [String, Number]
  },
  data() {
    return {
      showItemsCollapse: false
    };
  },
  computed: {
    showSpinner() {
      return this.roleStatus.roleLoading;
    }
  },
  methods: {
    itemsCollapseClick() {
      this.showItemsCollapse = !this.showItemsCollapse;
    }
  }
};
</script>