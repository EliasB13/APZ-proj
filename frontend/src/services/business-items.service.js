import { authHeader } from "../helpers";
import { userService } from "./user.service";

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
  ).then(handleResponse);
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
  ).then(handleResponse);
}

function _delete(id) {
  const requestOptions = {
    method: "DELETE",
    headers: authHeader()
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/SharedItems/${id}`,
    requestOptions
  ).then(handleResponse);
}

function handleResponse(response) {
  return response.text().then(text => {
    const data = text && JSON.parse(text);
    if (!response.ok) {
      if (response.status === 401) {
        //userService.logout();
        //location.reload(true);
      }

      const error = (data && data.message) || response.statusText;
      return Promise.reject(error);
    }

    return data;
  });
}
