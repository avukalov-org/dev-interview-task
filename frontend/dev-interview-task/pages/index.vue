<script lang="ts" setup>
const isLogin = ref(true);
const toggleLogin = () => (isLogin.value = !isLogin.value);
</script>

<template>
  <div class="flex flex-row justify-center mt-12 md:mt-24">
    <div class="flex flex-col gap-4">
      <button
        @click="toggleLogin"
        :class="{
          'btn btn-neutral w-1/2': true,
          'self-end': !isLogin,
        }"
      >
        {{ isLogin ? "Register" : "Login" }}
      </button>
      <AppLogin v-if="isLogin" />
      <AppRegister v-else />
    </div>
  </div>
</template>
