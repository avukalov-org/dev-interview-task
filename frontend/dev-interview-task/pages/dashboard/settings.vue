<script lang="ts" setup>
definePageMeta({
  layout: "dashboard",
  middleware: ["authenticated"],
});

const { user } = useUserSession();
</script>

<template>
  <h1>Settings</h1>
</template>
