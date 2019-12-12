import { authHeader } from "../helpers";
import { hasMagic } from "glob";

export const rolesService = {
  getRoles,
  addRole,
  removeRole,
  addEmployeeToRole,
  removeEmployeeFromRole,
  getEmployeesByRole
};

function getRoles() {
  const requestOptions = {
    method: "GET",
    headers: authHeader()
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/EmployeesRoles/business-roles`,
    requestOptions
  ).then(handleResponse);
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
  ).then(handleResponse);
}

function removeRole(id) {
  const requestOptions = {
    method: "DELETE",
    headers: authHeader()
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/EmployeesRoles/employees-role/${id}`,
    requestOptions
  ).then(handleResponse);
}

function getEmployeesByRole(roleId) {
  const requestOptions = {
    method: "GET",
    headers: authHeader()
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/EmployeesRoles/employees-in-role/${roleId}`,
    requestOptions
  ).then(handleResponse);
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
  ).then(handleResponse);
}

function removeEmployeeFromRole(emplId, roleId) {
  const requestOptions = {
    method: "DELETE",
    headers: authHeader()
  };

  return fetch(
    `${process.env.VUE_APP_DEV_BACKEND_URL}/api/EmployeesRoles/employee-in-role/${roleId}/${emplId}`,
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
