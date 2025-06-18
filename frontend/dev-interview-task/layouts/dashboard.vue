<script lang="ts" setup>
const { loggedIn, clear: clearSession } = useUserSession();
async function logout() {
  await clearSession();
  await navigateTo("/");
}
</script>

<template>
  <div class="flex min-h-screen flex-col">
    <AppNavBar />
    <div>
      <div class="drawer lg:drawer-open">
        <input
          id="my-drawer-2"
          type="checkbox"
          class="drawer-toggle"
        >
        <div class="drawer-content flex flex-col items-center justify-center">
          <!-- Page content here -->
          <label for="my-drawer-2" class="drawer-button lg:hidden self-start">
            <Icon
              name="tabler:menu-2"
              size="32"
              class="ml-4 mt-2"
            />
          </label>
          <slot />
        </div>
        <div class="drawer-side">
          <label
            for="my-drawer-2"
            aria-label="close sidebar"
            class="drawer-overlay"
          />
          <ul class="menu bg-base-200 text-base-content min-h-full w-48 p-4">
            <!-- Sidebar content here -->
            <li>
              <NuxtLink to="/dashboard">
                Home
              </NuxtLink>
            </li>
            <li>
              <NuxtLink to="/dashboard/videos">
                My Videos
              </NuxtLink>
            </li>
            <li>
              <NuxtLink to="/dashboard/settings">
                Settings
              </NuxtLink>
            </li>
            <li>
              <a @click="logout">
                Logout
              </a>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </div>
</template>
