<script lang="ts" setup>
import type { Video } from "types";

import "@mux/mux-player";

definePageMeta({
  layout: "dashboard",
  middleware: ["authenticated"],
});
const runtimeConfig = useRuntimeConfig();
const { session } = useUserSession();

const { data: videos, status } = await useFetch<Video[]>(
  `${runtimeConfig.public.API_BASE_URL}/api/videos/user`,
  {
    lazy: true,
    onRequest({ options }) {
      options.headers.set(
        "Authorization",
        `Bearer ${session.value.session.accessToken}`,
      );
    },
  },
);
</script>

<template>
  <div class="flex flex-col h-full w-full p-4 gap-4">
    <div class="flex flex-row justify-end w-full">
      <NuxtLink to="/dashboard/videos/create" class="btn btn-primary">
        <Icon
          name="tabler:video-plus"
          size="24"
          class="mr-2"
        /> Add new Video
      </NuxtLink>
    </div>
    <div v-if="status === 'pending'">
      <span class="loading loading-spinner loading-xl" />
    </div>
    <div
      v-else
      class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-4 p-4"
    >
      <div
        v-for="video in videos"
        :key="video.id"
        class="mb-4 break-inside-avoid flex flex-row justify-center"
      >
        <!-- <AppPremiumCard v-if="video.isPremium" :video="video" /> -->
        <AppUserCard :video="video" />
      </div>
    </div>
  </div>
</template>
