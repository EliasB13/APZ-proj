import { authHeader } from "../helpers";
import { responseHandler } from "../helpers";

export const readersService = {
  getReaders,
  orderReader,

  addItemToReader,
  removeItemFromReader,
  getReaderItems
};

function getReaders() {
  const requestOptions = {
    method: "GET",
    headers: authHeader()
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/Readers/readers`,
    requestOptions
  ).then(responseHandler.handleResponse);
}

function orderReader() {
  const requestOptions = {
    method: "POST",
    headers: { ...authHeader(), "Content-Type": "application/json" }
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/Readers/order-reader`,
    requestOptions
  ).then(responseHandler.handleResponse);
}

function addItemToReader(itemId, readerId) {
  const requestOptions = {
    method: "POST",
    headers: { ...authHeader(), "Content-Type": "application/json" },
    body: JSON.stringify({ sharedItemId: itemId, readerId: readerId })
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/Readers/add-item-to-reader`,
    requestOptions
  ).then(responseHandler.handleResponse);
}

function removeItemFromReader(itemId) {
  const requestOptions = {
    method: "DELETE",
    headers: { ...authHeader(), "Content-Type": "application/json" }
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/Readers/remove-item-from-reader/${itemId}`,
    requestOptions
  ).then(responseHandler.handleResponse);
}

function getReaderItems(readerId) {
  const requestOptions = {
    method: "GET",
    headers: { ...authHeader(), "Content-Type": "application/json" }
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/Readers/reader-items/${readerId}`,
    requestOptions
  ).then(responseHandler.handleResponse);
}
