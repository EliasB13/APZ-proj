import { authHeader } from "../helpers";
import { responseHandler } from "../helpers";

export const rolesService = {
  getRoles,
  getRoleById,
  addRole,
  removeRole,

  addEmployeeToRole,
  removeEmployeeFromRole,
  getEmployeesByRole,

  getItemsInRole,
  addItemToRole,
  removeItemFromRole
};

function getRoles() {
  const requestOptions = {
    method: "GET",
    headers: authHeader()
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/EmployeesRoles/business-roles`,
    requestOptions
  ).then(responseHandler.handleResponse);
}

function getRoleById(roleId) {
  const requestOptions = {
    method: "GET",
    headers: authHeader()
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/EmployeesRoles/role/${roleId}`,
    requestOptions
  ).then(responseHandler.handleResponse);
}

function addRole(role) {
  const requestOptions = {
    method: "POST",
    headers: { ...authHeader(), "Content-Type": "application/json" },
    body: JSON.stringify({ name: role.name, description: role.description })
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/EmployeesRoles/create-employees-role`,
    requestOptions
  ).then(responseHandler.handleResponse);
}

function removeRole(id) {
  const requestOptions = {
    method: "DELETE",
    headers: authHeader()
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/EmployeesRoles/employees-role/${id}`,
    requestOptions
  ).then(responseHandler.handleResponse);
}

function getEmployeesByRole(roleId) {
  const requestOptions = {
    method: "GET",
    headers: authHeader()
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/EmployeesRoles/employees-in-role/${roleId}`,
    requestOptions
  ).then(responseHandler.handleResponse);
}

function addEmployeeToRole(emplId, roleId) {
  const requestOptions = {
    method: "POST",
    headers: { ...authHeader(), "Content-Type": "application/json" },
    body: JSON.stringify({ employeeId: emplId, roleId: roleId })
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/EmployeesRoles/add-employee-to-role`,
    requestOptions
  ).then(responseHandler.handleResponse);
}

function removeEmployeeFromRole(emplId, roleId) {
  const requestOptions = {
    method: "DELETE",
    headers: authHeader()
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/EmployeesRoles/employee-in-role/${roleId}/${emplId}`,
    requestOptions
  ).then(responseHandler.handleResponse);
}

function getItemsInRole(roleId) {
  const requestOptions = {
    method: "GET",
    headers: authHeader()
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/SharedItems/employees-role-items/${roleId}`,
    requestOptions
  ).then(responseHandler.handleResponse);
}

function addItemToRole(itemId, roleId) {
  const requestOptions = {
    method: "POST",
    headers: { ...authHeader(), "Content-Type": "application/json" },
    body: JSON.stringify({ roleId: roleId, sharedItemId: itemId })
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/SharedItems/add-item-to-employees-role`,
    requestOptions
  ).then(responseHandler.handleResponse);
}

function removeItemFromRole(roleItemId) {
  const requestOptions = {
    method: "DELETE",
    headers: authHeader()
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/SharedItems/item-in-role/${roleItemId}`,
    requestOptions
  ).then(responseHandler.handleResponse);
}
