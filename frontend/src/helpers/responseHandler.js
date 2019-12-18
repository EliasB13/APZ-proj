import { userService } from "../services/user.service";
import i18n from "../localization/i18n";

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

      //const error = parseError(data, response.statusText);
      if (data.code) {
        const error = getLocaleError(data);
        return Promise.reject(error);
      } else {
        const error = parseModelErrors(data);
        return Promise.reject(error);
      }
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

function parseModelErrors(data) {
  var errors = [];
  Object.keys(data.errors).forEach((key, index) =>
    errors.push(data.errors[key])
  );
  const errorString = errors.join("\n");
  return errorString;
}

function getLocaleError(data) {
  debugger;
  if (data.code == 999) return data.message;
  return i18n.t("errorCode." + data.code.toString());
}
