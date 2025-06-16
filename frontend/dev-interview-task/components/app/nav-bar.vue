<script lang="ts" setup>
const { loggedIn, clear: clearSession } = useUserSession();

async function logout() {
  await clearSession();
  await navigateTo("/");
}
</script>

<template>
  <div class="navbar bg-neutral text-primary-content">
    <div class="navbar-start">
      <NuxtLink to="/" class="text-xl ml-4">DevInterviewTask</NuxtLink>
    </div>
    <div class="navbar-end">
      <AppThemeToggle />
      <button v-if="loggedIn" @click="logout" class="btn btn-primary">
        Logout
      </button>
    </div>
  </div>
</template>
