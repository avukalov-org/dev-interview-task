<script lang="ts" setup>
import type { User } from "~/models/user";

definePageMeta({
  middleware: ["authenticated"],
});

const { user } = useUserSession();
const typedUser = computed(() => user.value as User);
</script>

<template>
  <h1>Settings</h1>
</template>
