import { authHeader } from "../helpers";
import { responseHandler } from "../helpers";

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
  ).then(responseHandler.handleResponse);
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
  ).then(responseHandler.handleResponse);
}

function removeEmployee(id) {
  const requestOptions = {
    method: "DELETE",
    headers: authHeader()
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/Employees/employee/${id}`,
    requestOptions
  ).then(responseHandler.handleResponse);
}

function getUserByLogin(login) {
  const requestOptions = {
    method: "GET",
    headers: authHeader()
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/PrivateUsers/public-profile?login=${login}`,
    requestOptions
  ).then(responseHandler.handleResponse);
}
