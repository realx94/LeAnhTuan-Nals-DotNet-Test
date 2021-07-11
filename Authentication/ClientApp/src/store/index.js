import Vue from "vue";
import Vuex from "vuex";
import module_users from "./modules/users";
Vue.use(Vuex);

export default new Vuex.Store({
	modules: {
		users: module_users,
	}
});
