import { authHeader } from "../helpers";

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
  ).then(handleResponse);
}

function getItemsByBusiness(businessUserId) {
  const requestOptions = {
    method: "GET",
    headers: authHeader()
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/SharedItems?businessUserId=${businessUserId}`,
    requestOptions
  ).then(handleResponse);
}

function getAvailableServices() {
  const requestOptions = {
    method: "GET",
    headers: authHeader()
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/PrivateUsers/available-services`,
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
