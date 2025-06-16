<script lang="ts" setup>
import type { User } from "~/models/user";

definePageMeta({
  middleware: ["authenticated"],
});

const { user } = useUserSession();
const typedUser = computed(() => user.value as User);
</script>

<template>
  <h1>Dashboard Page</h1>
  <div>
    <h1 v-if="user">Welcome {{ typedUser.firstName }}</h1>
  </div>
</template>
