<script setup lang="ts">
const { loggedIn, user, fetch: refreshSession } = useUserSession();
const credentials = reactive({
  firstName: "User",
  lastName: "User",
  email: "user@nextmail.com",
  password: "123456",
  confirmPassword: "123456",
});
async function register() {
  $fetch("/api/auth/register", {
    method: "POST",
    body: credentials,
  })
    .then(async () => {
      // Refresh the session on client-side and redirect to the home page
      await refreshSession();
      await navigateTo("/dashboard");
    })
    .catch(() => alert("Bad Request"));
}
</script>

<template>
  <form @submit.prevent="register">
    <fieldset
      class="fieldset bg-base-200 border-base-300 rounded-box w-xs border p-4"
    >
      <legend class="fieldset-legend">Register</legend>

      <label class="label">Firstname</label>
      <input
        v-model="credentials.firstName"
        type="text"
        class="input"
        placeholder="Firstname"
      />

      <label class="label">Lastname</label>
      <input
        v-model="credentials.lastName"
        type="text"
        class="input"
        placeholder="Lastname"
      />

      <label class="label">Email</label>
      <input
        v-model="credentials.email"
        type="email"
        class="input"
        placeholder="Email"
      />

      <label class="label">Password</label>
      <input
        v-model="credentials.password"
        type="password"
        class="input"
        placeholder="Password"
      />

      <label class="label">Confirm Password</label>
      <input
        v-model="credentials.confirmPassword"
        type="password"
        class="input"
        placeholder="ConfirmPassword"
      />

      <button class="btn btn-neutral mt-4">Register</button>
    </fieldset>
  </form>
</template>
