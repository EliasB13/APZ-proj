import { authHeader } from "../helpers";
import { responseHandler } from "../helpers";

export const businessItemsService = {
  getItems,
  addItem,
  delete: _delete
};

function getItems() {
  const requestOptions = {
    method: "GET",
    headers: authHeader()
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/SharedItems`,
    requestOptions
  ).then(responseHandler.handleResponse);
}

function addItem(item) {
  const requestOptions = {
    method: "POST",
    headers: { ...authHeader(), "Content-Type": "application/json" },
    body: JSON.stringify({ name: item.name, description: item.description })
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/SharedItems`,
    requestOptions
  ).then(responseHandler.handleResponse);
}

function _delete(id) {
  const requestOptions = {
    method: "DELETE",
    headers: authHeader()
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/SharedItems/${id}`,
    requestOptions
  ).then(responseHandler.handleResponse);
}
