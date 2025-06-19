<script lang="ts" setup>
import type { Video } from "types";

import "@mux/mux-player";
import type { User } from "#auth-utils";

definePageMeta({
  layout: "dashboard",
  middleware: ["authenticated"],
});
const runtimeConfig = useRuntimeConfig();
const { user, session } = useUserSession();

const { data: videos, status } = await useFetch<Video[]>(
  `${runtimeConfig.public.API_BASE_URL}/api/videos`,
  {
    lazy: true,
    onRequest({ options }) {
      options.headers.set(
        "Authorization",
        `Bearer ${session.value.session.accessToken}`
      );
    },
  }
);

watch(videos, console.log);

const checkPremium = (video: Video) =>
  video.isPremium && video.userId !== user.value.id && !video.isPurchased;
</script>

<template>
  <div class="flex flex-col h-full w-full p-4 gap-4">
    <div class="flex flex-row justify-end w-full">
      <NuxtLink to="/dashboard/videos/create" class="btn btn-primary">
        <Icon name="tabler:video-plus" size="24" class="mr-2" /> Add new Video
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
        <AppPremiumCard v-if="checkPremium(video)" :video="video" />
        <AppCard v-else :video="video" />
      </div>
    </div>
  </div>
</template>
