<template>
  <div>
    <h2>Register</h2>
    <form @submit.prevent="handleSubmit" class="form-wrap">
      <div class="form-group">
        <label for="username">Username</label>
        <input
          type="text"
          v-model="user.username"
          name="username"
          class="form-control"
        />
      </div>
      <div class="form-group">
        <label htmlFor="password">Password</label>
        <input
          type="password"
          v-model="user.password"
          name="password"
          class="form-control"
        />
      </div>
      <div class="form-group">
        <label htmlFor="password">Type</label>
        <select v-model="userType" class="custom-select">
          <option value="0">User</option>
          <option value="1">Admin</option>
          <option value="2">Partner</option>
        </select>
      </div>
      <div class="form-group">
        <button class="btn btn-primary">Register</button>
        <router-link to="/login" class="btn btn-link">Login</router-link>
      </div>
    </form>
  </div>
</template>

<script>
import { mapActions } from "vuex";
export default {
  data() {
    return {
      user: {
        username: "",
        password: "",
        type: 0,
      },
      submitted: false,
    };
  },
  computed: {
    userType: {
      get: function () {
        return this.user.type;
      },
      set: function (value) {
        this.user.type = parseInt(value || "0");
      },
    },
  },
  methods: {
    ...mapActions("users", ["register"]),
    handleSubmit() {
      this.register(this.user)
        .then(function (data) {
          if (data.success) {
            alert("Successlly created user!");
          } else {
            alert(data.message);
          }
        })
        .catch(function (err) {
          alert(err);
        });
    },
  },
};
</script>