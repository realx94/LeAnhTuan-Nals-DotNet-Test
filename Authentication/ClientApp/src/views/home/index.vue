<template>
  <div class="hello">
    <h1>This is Home page</h1>
    <h2>Hello {{ user.userName }}</h2>
    <h2>Account type is {{ user.typeName }}</h2>
    <h2 class="info">Result from API: {{ apiResult }}</h2>
    <h3 v-if="user.type == '0'">
      This line will show to user with type is User
    </h3>
    <h3 v-else-if="user.type == '1'">
      This line will show to user with type is Admin
    </h3>
    <h3 v-else-if="user.type == '2'">
      This line will show to user with type is Partner
    </h3>
    <router-link to="/misc" class="btn btn-link">Go to Misc Page</router-link>
  </div>
</template>

<script>
import { mapActions } from "vuex";

export default {
  name: "HelloWorld",
  computed: {
    user() {
      return this.$store.state.users.user;
    },
  },
  data() {
    return {
      apiResult: "",
    };
  },
  methods: {
    ...mapActions("users", ["testUser"]),
  },
  created() {
    this.testUser().then((data) => {
      if (data.success) this.apiResult = data.result;
      else this.apiResult = data.message;
    });
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
