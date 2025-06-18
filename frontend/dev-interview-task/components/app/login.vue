<script setup lang="ts">
const { loggedIn, user, fetch: refreshSession } = useUserSession();

const credentials = reactive({
  email: "user@nextmail.com",
  password: "123456",
  rememberMe: true,
});

async function login() {
  $fetch("/api/auth/login", {
    method: "POST",
    body: credentials,
  })
    .then(async () => {
      // Refresh the session on client-side and redirect to the home page
      await refreshSession();
      await navigateTo("/dashboard");
    })
    .catch(() => alert("Bad credentials"));
}
</script>

<template>
  <div class="flex flex-col gap-4 w-xs">
    <form @submit.prevent="login">
      <fieldset
        class="fieldset bg-base-200 border-base-300 rounded-box w-xs border p-4"
      >
        <legend class="fieldset-legend">
          Login
        </legend>

        <label class="label">
          Email
        </label>
        <input
          v-model="credentials.email"
          type="email"
          class="input"
          placeholder="Email"
        >

        <label class="label">
          Password
        </label>
        <input
          v-model="credentials.password"
          type="password"
          class="input"
          placeholder="Password"
        >

        <label class="label mt-2">
          <input
            v-model="credentials.rememberMe"
            type="checkbox"
            :checked="credentials.rememberMe"
            class="checkbox"
          >
          Remember me
        </label>

        <button class="btn btn-neutral mt-4">
          Login
        </button>
      </fieldset>
    </form>
    <NuxtLink
      to="/api/auth/google"
      external
      class="btn btn-primary"
    >
      Login with Google <Icon name="tabler:brand-google-filled" size="18" />
    </NuxtLink>
  </div>
</template>
