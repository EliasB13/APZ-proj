import { userService } from "../services/user.service";

export const responseHandler = {
  handleResponse,
  parseError
};

function handleResponse(response) {
  return response.text().then(text => {
    const data = text && JSON.parse(text);
    if (!response.ok) {
      if (response.status === 401) {
        userService.logout();
        location.reload(true);
      }

      const error = parseError(data, response.statusText);
      return Promise.reject(error);
    }

    return data;
  });
}

function parseError(data, statusText) {
  if (data.errors) {
    var errors = [];
    Object.keys(data.errors).forEach((key, index) =>
      errors.push(data.errors[key])
    );
    const errorString = errors.join("\n");
    return errorString;
  }
  var error = (data && data.message) || statusText;
  return error;
}
