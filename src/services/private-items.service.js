import { authHeader } from "../helpers";
import { responseHandler } from "../helpers";

export const privateItemsService = {
  getActiveItems,
  getItemsByBusiness,
  getAvailableServices
};

function getActiveItems() {
  const requestOptions = {
    method: "GET",
    headers: authHeader()
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/PrivateUsers/active-items`,
    requestOptions
  ).then(responseHandler.handleResponse);
}

function getItemsByBusiness(businessUserId) {
  const requestOptions = {
    method: "GET",
    headers: authHeader()
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/SharedItems?businessUserId=${businessUserId}`,
    requestOptions
  ).then(responseHandler.handleResponse);
}

function getAvailableServices() {
  const requestOptions = {
    method: "GET",
    headers: authHeader()
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/PrivateUsers/available-services`,
    requestOptions
  ).then(responseHandler.handleResponse);
}
