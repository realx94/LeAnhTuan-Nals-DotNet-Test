import api from "../../../api/users";

const actions = {
    login({ commit }, data) {
        return new Promise(function (resolve, reject) {
            api.login(data)
                .then(function (response) {
                    if (response.data.success) {
                        commit("loginSuccess", response.data.result);
                        resolve(response.data);
                    } else {
                        reject(response.data);
                    }
                })
                .catch(function (err) {
                    reject(err);
                });
        });
    },
    logout({ commit }) {
        commit("logout");
    },
    register({ commit }, data) {
        return new Promise(function (resolve, reject) {
            api.create(data).then(function (response) {
                resolve(response.data);
            }).catch(function (err) {
                reject(err);
                commit();
            });
        });
    },
    reloadUser({ commit }) {
        commit("UpdateUser");
    },
    testUser({ commit }) {
        return new Promise(function (resolve, reject) {
            api.test().then(function (response) {
                resolve(response.data);
            }).catch(function (err) {
                reject(err)
                commit();
            });
        });
    }
}

const mutations = {
    loginSuccess(state, data) {
        localStorage.setItem("userToken", data.token);
        localStorage.setItem("user", JSON.stringify(data.user));
        state.user = Object.assign({}, state.user, data.user);
        state.user.isAuthorized = true;
    },
    logout(state) {
        state.appStatus = "loading";
        state.user.isAuthorized = false;
        localStorage.removeItem("userToken");
        localStorage.removeItem("user");
        location.href = location.origin;
    },
    UpdateUser(state) {
        console.log(12);
        var user = JSON.parse(localStorage.getItem("user") || "{}");
        state.user = Object.assign({}, state.user, user);
        state.user.isAuthorized = true;
    }
}
export { actions, mutations };