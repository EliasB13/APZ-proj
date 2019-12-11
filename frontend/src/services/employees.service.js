import { authHeader } from "../helpers";

export const employeesService = {
  getEmployees,
  addEmployee,
  removeEmployee,
  getUserByLogin
};

function getEmployees() {
  const requestOptions = {
    method: "GET",
    headers: authHeader()
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/Employees/businessEmployees`,
    requestOptions
  ).then(handleResponse);
}

function addEmployee(login) {
  const requestOptions = {
    method: "POST",
    headers: { ...authHeader(), "Content-Type": "application/json" },
    body: JSON.stringify({ login })
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/Employees/add-employee`,
    requestOptions
  ).then(handleResponse);
}

function removeEmployee(id) {
  const requestOptions = {
    method: "DELETE",
    headers: authHeader()
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/Employees/employee/${id}`,
    requestOptions
  ).then(handleResponse);
}

function getUserByLogin(login) {
  const requestOptions = {
    method: "GET",
    headers: authHeader()
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/PrivateUsers/public-profile?login=${login}`,
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
