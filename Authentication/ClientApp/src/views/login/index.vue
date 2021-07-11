<template>
  <div>
    <h2>Login</h2>
    <form @submit.prevent="handleSubmit" class="form-wrap">
      <div class="form-group">
        <label for="username">Username</label>
        <input
          type="text"
          v-model="username"
          name="username"
          class="form-control"
          :class="{ 'is-invalid': !username && isSubmited }"
        />
        <div v-show="!username && isSubmited" class="invalid-feedback">
          Username is required
        </div>
      </div>
      <div class="form-group">
        <label htmlFor="password">Password</label>
        <input
          type="password"
          v-model="password"
          name="password"
          class="form-control"
          :class="{ 'is-invalid': !password && isSubmited }"
        />
        <div v-show="!password && isSubmited" class="invalid-feedback">
          Password is required
        </div>
      </div>
      <div class="form-group">
        <button class="btn btn-primary">Login</button>
        <router-link to="/register" class="btn btn-link">Register</router-link>
      </div>
    </form>
  </div>
</template>

<script>
import { mapActions } from "vuex";

export default {
  data() {
    return {
      username: "",
      password: "",
      isSubmited: false,
      callbackUrl: location.origin,
    };
  },
  computed: {},
  created() {
    this.callbackUrl += (this.$route.query.callbackUrl || "");
  },
  methods: {
    ...mapActions("users", ["login", "logout"]),
    handleSubmit() {
      let self = this;
      self.isSubmited = true;
      if (self.username && self.password) {
        self
          .login({ username: self.username, password: self.password })
          .then(() => {
            location.href = self.callbackUrl;
          });
      }
    },
  },
};
</script>