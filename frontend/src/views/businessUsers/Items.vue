<template>
  <div>
    <base-header type="gradient-success" class="pb-6 pb-8 pt-5 pt-md-8">
      <div class="container-fluid d-flex align-items-center">
        <div class="row">
          <div class="col-lg-7 col-md-10">
            <p class="text-white mt-0 mb-5">
              This is your items page. Here you can explore or manage your items
            </p>
          </div>
        </div>
      </div>
    </base-header>

    <div class="container-fluid mt--7">
      <base-cards-list :itemsList="items"></base-cards-list>
    </div>
  </div>
</template>

<script>
import BaseCardsList from "../../components/BaseCardsList";

export default {
  components: {
    BaseCardsList
  },
  data() {
    return {
      items: [
        {
          id: 1,
          name: "Mixer3000",
          description: "Just a beautiful mixer",
          isTaken: false
        },
        {
          id: 2,
          name: "Thickness gauge",
          description: "Useful item for car buyers",
          isTaken: false
        },
        {
          id: 3,
          name: "Magic stick",
          description: "An amazing stick for doing some wizardry",
          isTaken: true
        },
        {
          id: 4,
          name: "Magic stick",
          description: "An amazing stick for doing some wizardry",
          isTaken: true
        },
        {
          id: 5,
          name: "Magic stick",
          description: "An amazing stick for doing some wizardry",
          isTaken: true
        },
        {
          id: 6,
          name: "Magic stick",
          description: "An amazing stick for doing some wizardry",
          isTaken: true
        },
        {
          id: 7,
          name: "Magic stick",
          description: "An amazing stick for doing some wizardry",
          isTaken: true
        }
      ]
    };
  }
};
</script>
