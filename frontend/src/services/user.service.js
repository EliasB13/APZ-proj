import { authHeader } from "../helpers";
import { responseHandler } from "../helpers";

export const userService = {
  login,
  logout,
  register,
  getPublicProfile,
  update,
  delete: _delete,
  getAccountData
};

function login(login, password, isBusinessUser) {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ login, password })
  };

  var loginEndpoint = isBusinessUser
    ? "BusinessUsers/authenticate-business"
    : "PrivateUsers/authenticate-private";
  const requestString = `${process.env.VUE_APP_DEV_BACKEND_URL}/${loginEndpoint}`;

  return fetch(requestString, requestOptions)
    .then(responseHandler.handleResponse)
    .then(user => {
      if (user.token) {
        user.isBusinessUser = isBusinessUser;
        localStorage.setItem("user", JSON.stringify(user));
      }

      return user;
    });
}

function logout() {
  localStorage.removeItem("user");
}

function register(user, isBusinessUser) {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(user)
  };

  var registerEndpoint = isBusinessUser
    ? "BusinessUsers/register-business"
    : "PrivateUsers/register-private";
  const requestString = `${process.env.VUE_APP_DEV_BACKEND_URL}/${registerEndpoint}`;

  return fetch(requestString, requestOptions).then(
    responseHandler.handleResponse
  );
}

function getAccountData(isBusinessUser) {
  const requestOptions = {
    method: "GET",
    headers: authHeader()
  };

  var endpointString = isBusinessUser
    ? "BusinessUsers/account-data"
    : "PrivateUsers/account-data";
  const requestString = `${process.env.VUE_APP_DEV_BACKEND_URL}/${endpointString}`;

  return fetch(requestString, requestOptions).then(
    responseHandler.handleResponse
  );
}

function getPublicProfile(isBusinessUser, userId) {
  const requestOptions = {
    method: "GET",
    headers: authHeader()
  };

  var endpointString = isBusinessUser
    ? "BusinessUsers/public-profile"
    : "PrivateUsers/public-profile";
  const requestString = `${process.env.VUE_APP_DEV_BACKEND_URL}/${endpointString}?id=${userId}`;
  return fetch(requestString, requestOptions).then(
    responseHandler.handleResponse
  );
}

function update(user, isBusinessUser) {
  const requestOptions = {
    method: "PUT",
    headers: { ...authHeader(), "Content-Type": "application/json" },
    body: JSON.stringify(user)
  };

  var endpoint = isBusinessUser ? "BusinessUsers" : "PrivateUsers";

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/${endpoint}`,
    requestOptions
  ).then(responseHandler.handleResponse);
}

function _delete(id) {
  const requestOptions = {
    method: "DELETE",
    headers: authHeader()
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/users/${id}`,
    requestOptions
  ).then(responseHandler.handleResponse);
}
