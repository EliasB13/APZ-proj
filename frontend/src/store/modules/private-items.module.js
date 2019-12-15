import { privateItemsService } from "../../services";

const state = {
  activeItems: [],
  availableServices: [],
  status: {},
  businessItems: [],
  error: {}
};

const actions = {
  getActiveItems({ commit, dispatch }) {
    commit("getActiveItemsRequest");

    privateItemsService.getActiveItems().then(
      items => commit("getActiveItemsSuccess", items),
      error => {
        commit("getActiveItemsFailure", error);
        dispatch("alert/error", error, { root: true });
      }
    );
  },
  getAvailableServices({ commit, dispatch }) {
    commit("getAvailableServicesRequest");

    privateItemsService.getAvailableServices().then(
      items => commit("getAvailableItemsSuccess", items),
      error => {
        commit("getAvailabelItemsFailure", error);
        dispatch("alert/error", error, { root: true });
      }
    );
  },
  getBusinessItemsByBusinessUser({ commit, dispatch }, businessUserId) {
    commit("getBusinessItemsRequest", businessUserId);

    privateItemsService.getItemsByBusiness(businessUserId).then(
      items => commit("getBusinessItemsSucess", items),
      error => {
        commit("getBusinessItemsFailure", error);
        dispatch("alert/error", error, { root: true });
      }
    );
  }
};

const mutations = {
  getActiveItemsRequest(state) {
    state.status = { activeItemsLoading: true };
  },
  getActiveItemsSuccess(state, items) {
    state.status = { activeItemsLoaded: true, activeItemsLoading: false };
    state.activeItems = items;
  },
  getActiveItemsError(state, error) {
    state.status = {
      ...state.status,
      activeItemsLoaded: false,
      activeItemsLoading: false
    };
    state.error = error;
    state.activeItems = [];
  },

  getAvailableServiceRequest(state) {
    state.status = {
      ...state.status,
      availableServicesLoading: false
    };
  },
  getAvailableServiceSuccess(state, services) {
    state.status = {
      ...state.status,
      availableServicesLoaded: true,
      availableServicesLoading: false
    };
    state.availableServices = services;
  },
  getAvailableServiceFailure(state, error) {
    state.status = {
      ...state.status,
      availableServicesLoaded: false,
      availableServicesLoading: false
    };
    state.availableServices = [];
    state.error = error;
  },

  getBusinessItemsRequest(state, businessUserId) {
    state.status = {
      ...state.status,
      buisnessItemsLoading: true
    };
  },
  getBusinessItemsSuccess(state, items) {
    state.status = {
      ...state.status,
      buisnessItemsLoaded: true,
      buisnessItemsLoading: false
    };
    state.businessItems = items;
  },
  getBusinessItemsFailure(state, error) {
    state.status = {
      ...state.status,
      buisnessItemsLoaded: false,
      buisnessItemsLoading: false
    };
    state.businessItems = [];
    state.error = error;
  }
};

export const privateItems = {
  namespaced: true,
  state,
  actions,
  mutations
};
