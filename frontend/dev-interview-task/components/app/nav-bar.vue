<script lang="ts" setup>
const { loggedIn, clear: clearSession } = useUserSession();

const open = ref(false);
const toggleOpen = () => (open.value = !open.value);

async function logout() {
  toggleOpen();
  await clearSession();
  await navigateTo("/");
}
</script>

<template>
  <div class="navbar bg-neutral text-primary-content relative">
    <div class="navbar-start">
      <h1 class="text-xl ml-4">DevInterviewTask</h1>
    </div>
    <div class="navbar-end">
      <div class="flex flex-row items-center gap-4">
        <AppThemeToggle />
        <Icon
          v-if="loggedIn"
          @click="toggleOpen"
          name="tabler:user"
          size="24"
          class="mr-4 cursor-pointer"
        />
      </div>
    </div>
    <div class="absolute top-18 right-2">
      <ul
        v-if="loggedIn && open"
        class="menu menu-lg bg-base-200 rounded-box w-36"
      >
        <li>
          <NuxtLink to="/dashboard" @click="toggleOpen">Dashboard</NuxtLink>
        </li>
        <li>
          <NuxtLink to="/dashboard/settings" @click="toggleOpen"
            >Settings</NuxtLink
          >
        </li>
        <li><a @click="logout">Logout</a></li>
      </ul>
    </div>
  </div>
</template>
