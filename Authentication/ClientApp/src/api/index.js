import axios from "axios";
import store from "../store";

async function install(Vue) {
    axios.defaults.baseURL = window.location.origin + "/api";
    Vue.prototype.$http = axios;
    axios.interceptors.response.use(undefined, function(err) {
		return new Promise(function() {
			if (
				err.status === 401 &&
				err.config &&
				!err.config.__isRetryRequest
			) {
				store.dispatch("users/logout");
			}
			throw err;
		});
	});

    const token = localStorage.getItem("userToken");
    if (token) {
        axios.defaults.headers.common["Authorization"] = "Bearer " + token;
        store.dispatch("users/reloadUser");
    }
}

export default { install };