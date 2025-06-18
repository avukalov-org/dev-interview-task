<script lang="ts" setup>
const isLogin = ref(true);
</script>

<template>
  <div class="flex flex-row justify-center mt-12 md:mt-24">
    <div class="flex flex-col gap-4">
      <button
        class="btn btn-neutral w-1/2"
        :class="{
          'self-end': !isLogin,
        }"
        @click="isLogin = !isLogin"
      >
        {{ isLogin ? "Register" : "Login" }}
      </button>
      <AppLogin v-if="isLogin" />
      <AppRegister v-else />
    </div>
  </div>
</template>
