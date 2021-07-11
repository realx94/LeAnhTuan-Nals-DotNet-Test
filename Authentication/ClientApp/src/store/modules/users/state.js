const state = {
    user: {
        isAuthorized: false,
    },
};
const getters = {
    userType: (state) => state.user.type,
    isAuthorized: (state) => state.user.isAuthorized,
    userName: (state) => state.user.userName,
};
export { state, getters };