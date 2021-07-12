<template>
  <div>
    <h1>This is MISC page</h1>
    <h2 class="info">Result from API: {{ apiResult }}</h2>
    <router-link to="/" class="btn btn-link">Go to Home Page</router-link>
  </div>
</template>
<script>
import { mapActions } from "vuex";

export default {
  name: "misc",
  data() {
    return {
      apiResult: "",
    };
  },
  methods: {
    ...mapActions("users", ["testUser"]),
  },
  mounted() {
    this.testUser().then((data) => {
      if (data.success) this.apiResult = data.result;
      else this.apiResult = data.message;
    });
  },
}
</script>
