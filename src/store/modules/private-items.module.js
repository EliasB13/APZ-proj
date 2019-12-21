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
        dispatch("alert/error", error.toString(), { root: true });
      }
    );
  },
  getAvailableServices({ commit, dispatch }) {
    commit("getAvailableServicesRequest");

    privateItemsService.getAvailableServices().then(
      items => commit("getAvailableServicesSuccess", items),
      error => {
        commit("getAvailableServicesFailure", error);
        dispatch("alert/error", error.toString(), { root: true });
      }
    );
  },
  getBusinessItemsByBusinessUser({ commit, dispatch }, businessUserId) {
    commit("getBusinessItemsRequest", businessUserId);

    privateItemsService.getItemsByBusiness(businessUserId).then(
      items => commit("getBusinessItemsSuccess", items),
      error => {
        commit("getBusinessItemsFailure", error);
        dispatch("alert/error", error.toString(), { root: true });
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
  getActiveItemsFailure(state, error) {
    state.status = {
      ...state.status,
      activeItemsLoaded: false,
      activeItemsLoading: false
    };
    state.error = error;
    state.activeItems = [];
  },

  getAvailableServicesRequest(state) {
    state.status = {
      ...state.status,
      availableServicesLoading: false
    };
  },
  getAvailableServicesSuccess(state, services) {
    state.status = {
      ...state.status,
      availableServicesLoaded: true,
      availableServicesLoading: false
    };
    state.availableServices = services;
  },
  getAvailableServicesFailure(state, error) {
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
      businessItemsLoaded: true,
      businessItemsLoading: false
    };
    state.businessItems = items;
  },
  getBusinessItemsFailure(state, error) {
    state.status = {
      ...state.status,
      businessItemsLoaded: false,
      businessItemsLoading: false
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
