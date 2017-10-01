import axios from "axios";

let HTTP = axios.create({
  baseURL: "http://localhost:48484/",
  headers: {
    Accept: "application/json"
  }
});

HTTP.interceptors.request.use(
  function(config) {
    return config;
  },
  function(error) {
    return Promise.reject(error);
  }
);

HTTP.extractData = function(result) {
  return result.data.Properties;
};

export { HTTP };
