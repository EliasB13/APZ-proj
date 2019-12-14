import { authHeader } from "../helpers";

export const userService = {
  login,
  logout,
  register,
  getById,
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
    .then(handleResponse)
    .then(user => {
      // login successful if there's a jwt token in the response
      if (user.token) {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        user.isBusinessUser = isBusinessUser;
        localStorage.setItem("user", JSON.stringify(user));
      }

      return user;
    });
}

function logout() {
  // remove user from local storage to log user out
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

  return fetch(requestString, requestOptions).then(handleResponse);
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

  return fetch(requestString, requestOptions).then(handleResponse);
}

function getById(id) {
  const requestOptions = {
    method: "GET",
    headers: authHeader()
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/users/${id}`,
    requestOptions
  ).then(handleResponse);
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
  ).then(handleResponse);
}

function _delete(id) {
  const requestOptions = {
    method: "DELETE",
    headers: authHeader()
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/users/${id}`,
    requestOptions
  ).then(handleResponse);
}

function handleResponse(response) {
  return response.text().then(text => {
    const data = text && JSON.parse(text);
    if (!response.ok) {
      if (response.status === 401) {
        // auto logout if 401 response returned from api
        logout();
        location.reload(true);
      }

      const error = (data && data.message) || response.statusText;
      return Promise.reject(error);
    }

    return data;
  });
}
